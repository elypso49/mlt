using System.Text.RegularExpressions;
using mlt.common.dtos.realdebrid;
using mlt.common.dtos.responses;
using mlt.common.dtos.rss;
using mlt.common.dtos.rss.enums;
using mlt.common.dtos.workflows;
using mlt.common.extensions;
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
        var cleanedTitle = rssFeedResult.Title!.CleanTitle();

        // Regex patterns for identifying seasons, specials, OVA/OAVs
        var fileNameRegex = !string.IsNullOrWhiteSpace(rssFlux?.FileNameRegex) ? rssFlux.FileNameRegex : @"S(\d{2})E(\d{2})";
        var seasonRegex = new Regex(@"SEASON\s*(\d+)", RegexOptions.IgnoreCase);
        var multipleSeasonsRegex = new Regex(@"(?:S|SEASON)\s*(\d{1,2})(?:\s*\+\s*(\d{1,2}))+", RegexOptions.IgnoreCase);
        var specialsRegex = new Regex(@"S(\d{2})SP(\d{1,2})-(\d{1,2})", RegexOptions.IgnoreCase);
        var oavRegex = new Regex(@"(OAV|OVA)\s*(\d{1,2})?", RegexOptions.IgnoreCase);

        string seasonFolder;

        // Check if the title matches an explicit episode pattern like SXXEYY
        if (Regex.Match(cleanedTitle, fileNameRegex) is { Success: true } match)
            seasonFolder = $"/Season {int.Parse(match.Groups[1].Value):00}";

        // Check for season definition like "Season X"
        else if (seasonRegex.Match(cleanedTitle) is { Success: true } matchSeason)
            seasonFolder = $"/Season {int.Parse(matchSeason.Groups[1].Value):00}";

        // Check for multiple seasons like "Season 1+2"
        else if (multipleSeasonsRegex.Match(cleanedTitle) is { Success: true } matchMultipleSeasons)
        {
            var seasonList = new List<string>();
            var matches = multipleSeasonsRegex.Matches(cleanedTitle);

            foreach (Match seasonMatch in matches)
                seasonList.Add($"/Season {int.Parse(seasonMatch.Groups[1].Value):00}");

            seasonFolder = string.Join(", ", seasonList);
        }

        // Check for specials like "S02SP01-02"
        else if (specialsRegex.Match(cleanedTitle) is { Success: true })
            seasonFolder = "/Specials";

        // Check if it's an OVA/OAV
        else if (oavRegex.Match(cleanedTitle) is { Success: true } matchOav)
            seasonFolder = "/Specials";

        // Default to Season 01 if no conditions match
        else
            seasonFolder = "/Season 01";

        var destinationFolder = $"{rssFlux?.DestinationFolder}/{(string.IsNullOrWhiteSpace(rssFeedResult.NyaaInfoHash) ? rssFeedResult.TvShowName : rssFlux?.Name)}";

        return (destinationFolder, seasonFolder);
    }
}