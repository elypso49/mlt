using System.Text.RegularExpressions;
using mlt.common.dtos.realdebrid;
using mlt.common.dtos.responses;
using mlt.common.dtos.rss;
using mlt.common.dtos.rss.enums;
using mlt.common.dtos.workflows;
using mlt.common.services;
using mlt.realdebrid.services;
using mlt.rss.services;
using mlt.synology.services;

namespace mlt.workflow.services;

public class WorkflowService(
    IRssFeedService rssFeedService,
    IRssFeedProcessorService rssFeedProcessorService,
    IRssFeedResultService rssFeedResultService,
    IRealDebridService realDebridService,
    IDownloadStationService downloadStationService) : BaseService, IWorkflowService
{
    public async Task<ResponseDto<WorkflowResponse>> DownloadAll()
    {
        ResponseDto<WorkflowResponse> response = new();
        try
        {
            var rssFeeds = HandleData(await rssFeedService.GetAll(), response).ToList();

            await Task.WhenAll(rssFeeds.Select(rssFeed => rssFeedProcessorService.ProcessFeed(rssFeed.Id!)));

            var rssFeedResults = HandleData(await rssFeedResultService.GetAll(), response).Where(x => x.State != StateValue.Downloaded).ToList();

            var torrentsId = HandleData(await realDebridService.AddTorrentsInBatchesWithRetry(rssFeedResults.Select(x => x.Link)), response);

            while (torrentsId.Any())
                torrentsId = await ProcessFeedWithRetry(torrentsId, rssFeeds, rssFeedResults);

            response.Data = new() { IsSuccess = true };
        }
        catch (Exception e)
        {
            return ManageError(e, response);
        }

        return response;
    }

    private async Task<List<AddTorrentResponse>> ProcessFeedWithRetry(
        List<AddTorrentResponse> torrentsId,
        IEnumerable<RssFeed>? rssFeeds,
        IEnumerable<RssFeedResult> rssFeedResults)
    {
        var torrentsResult = await realDebridService.GetTorrents() is { IsSuccess: true } torrentsResponse ? torrentsResponse.Data : null;
        if (torrentsResult == null)
            throw new Exception("Unable to retrieve torrents from RealDebrid");

        var currentTorrents = torrentsResult.Where(x => torrentsId.Any(y => y.TorrentId == x.Id));
        var torrents = currentTorrents.ToList();

        var downloadedTorrents = torrentsId.Where(x => torrents.First(y => y.Id == x.TorrentId).Status == "downloaded").ToList();
        var downloadingTorrents = torrentsId.Where(x => torrents.First(y => y.Id == x.TorrentId).Status != "downloaded").ToList();

        await Task.WhenAll(rssFeedResults.Select(rssFeedResult => ProcessFeedResult(downloadedTorrents, rssFeedResult, rssFeeds)));

        return downloadingTorrents;
    }

    private async Task ProcessFeedResult(List<AddTorrentResponse> torrentsId, RssFeedResult rssFeedResult, IEnumerable<RssFeed>? rssFeeds)
    {
        var currentTorrentId = torrentsId.FirstOrDefault(y => y.TorrentSource == rssFeedResult.Link)?.TorrentId;
        if (currentTorrentId == null)
            return;

        var torrentInfo = await realDebridService.GetTorrentInfo(currentTorrentId) is { IsSuccess: true } torrentInfoResult ? torrentInfoResult.Data : null;
        if (torrentInfo is null)
            return;

        var debridedLinks = await realDebridService.UnrestrictLinks(torrentInfo.Links) is { IsSuccess: true } unrestrictedLinksResult
            ? unrestrictedLinksResult.Data?.ToList()
            : null;
        if (debridedLinks is null || !debridedLinks.Any())
            return;

        var (destinationFolder, seasonFolder) = CalculateFolders(rssFeedResult, rssFeeds?.First(x => x.Id == rssFeedResult.RssFeedId));

        foreach (var _ in (await downloadStationService.CreateTask(debridedLinks.Select(x => x.Download), $"{destinationFolder}{seasonFolder}"))
                 .Data?.Where(createTaskResponse => createTaskResponse.IsSuccess))
        {
            rssFeedResult.State = StateValue.Downloaded;
            await rssFeedResultService.Update(rssFeedResult.Id!, rssFeedResult);
        }
    }

    private static (string destination, string season) CalculateFolders(RssFeedResult rssFeedResult, RssFeed? rssFlux)
    {
        var fileNameRegex = !string.IsNullOrWhiteSpace(rssFlux?.FileNameRegex) ? rssFlux.FileNameRegex : @"S(\d{2})E(\d{2})";

        var seasonFolder = Regex.Match(rssFeedResult.Title!, fileNameRegex) is { Success: true } match ? $"/Season {int.Parse(match.Groups[1].Value):00}" : string.Empty;

        seasonFolder = string.IsNullOrWhiteSpace(seasonFolder) && Regex.Match(rssFeedResult.Title!, @"Season (\d+)") is { Success: true } matchSeason
            ? $"/Season {int.Parse(matchSeason.Groups[1].Value):00}"
            : string.Empty;

        seasonFolder = string.IsNullOrWhiteSpace(seasonFolder) && rssFlux?.ForceFirstSeasonFolder == true ? "/Season 01" : seasonFolder;

        var destinationFolder = $"{rssFlux?.DestinationFolder}/{(string.IsNullOrWhiteSpace(rssFeedResult.NyaaInfoHash) ? rssFeedResult.TvShowName : rssFlux?.Name)}";
        return (destinationFolder, seasonFolder);
    }
}