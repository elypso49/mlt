using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;
using mlt.common.dtos.realdebrid;
using mlt.common.dtos.responses;
using mlt.common.options;
using mlt.common.services;
using mlt.realdebrid.clients;

namespace mlt.realdebrid.services;

internal partial class RealDebridService(IRealDebridHttpClient rdClient, IOptions<RealDebridOptions> options) : BaseService, IRealDebridService
{
    public Task<ResponseDto<IEnumerable<RealDebridTorrentInfo>>> GetDownloads()
        => HandleDataRetrievement(async () => await rdClient.GetDownloads());

    public Task<ResponseDto<IEnumerable<RealDebridTorrentInfo>>> GetTorrents()
        => HandleDataRetrievement(async () => await rdClient.GetTorrents());

    public Task<ResponseDto<IEnumerable<RealDebridTorrentInfo>>> UnrestrictLinks(IEnumerable<string> links)
        => HandleDataRetrievement<IEnumerable<RealDebridTorrentInfo>>(async () => await Task.WhenAll(links.Select(rdClient.UnrestrictLink).ToList()));

    public async Task<ResponseDto<List<AddTorrentResponse>>> AddTorrentsInBatchesWithRetry(
        IEnumerable<string>? torrentLinks,
        int batchSize = 2,
        int initialDelayMilliseconds = 1000,
        int maxRetries = 3,
        int maxConcurent = 30)
    {
        var results = new ResponseDto<List<AddTorrentResponse>> { Data = [] };
        var torrentLinksLst = torrentLinks?.ToList();

        if (torrentLinksLst is null || torrentLinksLst.Any() == false)
            return results;

        for (var i = 0; i < Math.Min(torrentLinksLst.Count, maxConcurent); i += batchSize)
        {
            var batch = torrentLinksLst.Skip(i).Take(batchSize).ToArray();
            var retries = 0;
            var success = false;

            while (!success && retries < maxRetries)
            {
                try
                {
                    var batchResults = await AddTorrents(batch);
                    results.Data.AddRange(batchResults.ToList());
                    success = true;
                }
                catch (HttpRequestException ex) when ((int)ex.StatusCode! == 429)
                {
                    retries++;
                    var delay = initialDelayMilliseconds * (int)Math.Pow(2, retries);
                    await Task.Delay(delay);
                }
            }

            if (!success)
                throw new Exception("Failed to process batch after multiple retries.");

            if (i + batchSize < torrentLinksLst.Count)
                await Task.Delay(initialDelayMilliseconds);
        }

        return results;
    }

    public Task<ResponseDto<RealDebridTorrentInfo>> GetTorrentInfo(string torrentId)
        => HandleDataRetrievement(async () => await rdClient.GetTorrentInfo(torrentId));

    private async Task<List<AddTorrentResponse>> AddTorrents(IEnumerable<string> torrents)
    {
        var addTorrentTasks = torrents.Select(AddTorrent);
        var results = await Task.WhenAll(addTorrentTasks);

        return results.ToList();
    }

    private async Task<AddTorrentResponse> AddTorrent(string torrentSource)
    {
        var torrentId = MagnetRegex().IsMatch(torrentSource) ? await rdClient.AddMagnet(torrentSource) : await rdClient.AddTorrent(torrentSource);
        var torrent = await rdClient.GetTorrentInfo(torrentId);

        var selectedFiles = string.Join(',', torrent.Files.Where(file => options.Value.ExtensionFilterList.Contains(Path.GetExtension(file.Path))).Select(file => file.Id));

        if (!string.IsNullOrWhiteSpace(selectedFiles))
            await rdClient.SelectTorrentFiles(torrentId, selectedFiles);

        return new() { TorrentId = torrentId, TorrentSource = torrentSource };
    }

    [GeneratedRegex(@"^magnet:\?xt=urn:.*$")]
    private static partial Regex MagnetRegex();
}