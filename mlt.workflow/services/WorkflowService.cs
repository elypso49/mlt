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
        var rssFluxResults = (await rssFeedResultService.GetAll()).ToList();

        var torrentsId = await realDebridService.AddTorrentsInBatchesWithRetry(rssFluxResults.Select(x => x.Link!));

        var lstBridedFiles = new List<string>();

        foreach (var torrentId in torrentsId)
        {
            var realDebridInfos = await realDebridService.GetTorrentInfo(torrentId);
            lstBridedFiles.AddRange(realDebridInfos.Links);
        }

        var debridedFiles = await realDebridService.UnrestrictLinks(lstBridedFiles.ToArray());

        await downloadStationService.CreateTask(debridedFiles.Select(x => x.Download).ToArray());

        return true;
    }
}