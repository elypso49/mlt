using System.Text.RegularExpressions;
using mlt.dtos.rss;
using mlt.dtos.rss.enums;
using mlt.realdebrid.services;
using mlt.rss.services;
using mlt.synology.services;

namespace mlt.workflow.services;

public interface IWorkflowService
{
    public Task<bool> DownloadAll();
}

public class WorkflowService(
    IRssFeedService rssFeedService,
    IRssFeedProcessorService rssFeedProcessorService,
    IRssFeedResultService rssFeedResultService,
    IRealDebridService realDebridService,
    IDownloadStationService downloadStationService) : IWorkflowService
{
    public async Task<bool> DownloadAll()
    {
        var rssFeeds = (await rssFeedService.GetAll()).ToList();
        await Task.WhenAll(rssFeeds.Select(rssFeed => rssFeedProcessorService.ProcessFeed(rssFeed.Id!)));

        var rssFeedResults = (await rssFeedResultService.GetAll()).Where(x => x.State != StateValue.Downloaded).ToList();

        var torrentsId = (await realDebridService.AddTorrentsInBatchesWithRetry(rssFeedResults.Select(x => x.Link!))).ToList();

        while (torrentsId.Any())
            torrentsId = await ProcessFeedWithRetry(torrentsId, rssFeeds, rssFeedResults);

        return true;
    }

    private async Task<List<(string torrentId, string torrentSource)>> ProcessFeedWithRetry(
        List<(string torrentId, string torrentSource)> torrentsId,
        List<RssFeed> rssFeeds,
        List<RssFeedResult> rssFeedResults)
    {
        var torrentsResult = await realDebridService.GetTorrents();
        var currentTorrents = torrentsResult.Where(x => torrentsId.Any(y => y.torrentId == x.Id));
        var torrents = currentTorrents.ToList();

        var downloadedTorrents = torrentsId.Where(x => torrents.First(y => y.Id == x.torrentId).Status == "downloaded").ToList();
        var downloadingTorrents = torrentsId.Where(x => torrents.First(y => y.Id == x.torrentId).Status != "downloaded").ToList();

        await Task.WhenAll(rssFeedResults.Select(rssFeedResult => ProcessFeedResult(downloadedTorrents, rssFeedResult, rssFeeds)));

        return downloadingTorrents;
    }

    private async Task ProcessFeedResult(List<(string torrentId, string torrentSource)> torrentsId, RssFeedResult rssFeedResult, List<RssFeed> rssFeeds)
    {
        var currentTorrentId = torrentsId.First(y => y.torrentSource == rssFeedResult.Link).torrentId;
        var torrentInfo = await realDebridService.GetTorrentInfo(currentTorrentId);

        var debridedLinks = await realDebridService.UnrestrictLinks(torrentInfo.Links);

        var rssFlux = rssFeeds.First(x => x.Id == rssFeedResult.RssFeedId);

        var fileNameRegex = !string.IsNullOrWhiteSpace(rssFlux.FileNameRegex) ? rssFlux.FileNameRegex : @"S(\d{2})E(\d{2})";

        var seasonFolder = Regex.Match(rssFeedResult.Title!, fileNameRegex) is { Success: true } match ? $"/Season {int.Parse(match.Groups[1].Value):00}" : string.Empty;

        seasonFolder = string.IsNullOrWhiteSpace(seasonFolder) && Regex.Match(rssFeedResult.Title!, @"Season (\d+)") is { Success: true } matchSeason
            ? $"/Season {int.Parse(matchSeason.Groups[1].Value):00}"
            : string.Empty;

        seasonFolder = string.IsNullOrWhiteSpace(seasonFolder) && rssFlux.ForceFirstSeasonFolder ? "/Season 01" : seasonFolder;

        var destinationFolder = $"{rssFlux.DestinationFolder}/{(string.IsNullOrWhiteSpace(rssFeedResult.NyaaInfoHash) ? rssFeedResult.TvShowName : rssFlux.Name)}";

        foreach (var (_, isSuccess) in await downloadStationService.CreateTask(debridedLinks.Select(x => x.Download).ToArray(), $"{destinationFolder}{seasonFolder}"))
        {
            if (isSuccess)
            {
                rssFeedResult.State = StateValue.Downloaded;
                await rssFeedResultService.Update(rssFeedResult.Id!, rssFeedResult);
            }
        }
    }
}