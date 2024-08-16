using System.Text.RegularExpressions;

namespace mlt.realdebrid.services;

internal partial class RealDebridService(IRealDebridHttpClient rdClient, IOptions<RealDebridOptions> options) : IRealDebridService
{
    public Task<IEnumerable<RealDebridTorrentInfo>> GetDownloads()
        => rdClient.GetDownloads();

    public Task<IEnumerable<RealDebridTorrentInfo>> GetTorrents()
        => rdClient.GetTorrents();

    public async Task<IEnumerable<RealDebridTorrentInfo>> UnrestrictLinks(IEnumerable<string> links)
        => await Task.WhenAll(links.Select(rdClient.UnrestrictLink).ToList());
    
    private async Task<List<(string torrentId, string torrentSource)>> AddTorrents(IEnumerable<string> torrents)
    {
        var addTorrentTasks = torrents.Select(AddTorrent);
        var results = await Task.WhenAll(addTorrentTasks);

        return results.ToList();
    }
    
    public async Task<IEnumerable<(string torrentId, string torrentSource)>> AddTorrentsInBatchesWithRetry(IEnumerable<string> torrentLinks, int batchSize = 2, int initialDelayMilliseconds = 1000, int maxRetries = 3, int maxConcurent = 30)
    {
        var torrentLinksLst = torrentLinks.ToList();
        var results = new List<(string torrentId, string torrentSource)>();

        try
        {
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
                        results.AddRange(batchResults);
                        success = true; // Successfully processed the batch
                    }
                    catch (HttpRequestException ex) when ((int)ex.StatusCode! == 429) // 429 Too Many Requests
                    {
                        retries++;
                        var delay = initialDelayMilliseconds * (int)Math.Pow(2, retries); // Exponential backoff
                        await Task.Delay(delay);
                    }
                }

                if (!success)
                    throw new Exception("Failed to process batch after multiple retries.");

                if (i + batchSize < torrentLinksLst.Count)
                    await Task.Delay(initialDelayMilliseconds); // Delay between batches
            }
        }
        catch (Exception e)
        {
            // Console.WriteLine(e);
            //
            // throw;
        }

        return results;
    }
    
    public Task<RealDebridTorrentInfo> GetTorrentInfo(string torrentId)
    => rdClient.GetTorrentInfo(torrentId);

    private async Task<(string torrentId, string torrentSource)> AddTorrent(string torrentSource)
    {
        var torrentId = MagnetRegex().IsMatch(torrentSource) ? await rdClient.AddMagnet(torrentSource) : await rdClient.AddTorrent(torrentSource);
        var torrent = await rdClient.GetTorrentInfo(torrentId);

        var selectedFiles = string.Join(',', torrent.Files.Where(file => options.Value.ExtensionFilterList.Contains(Path.GetExtension(file.Path))).Select(file => file.Id));

        if (!string.IsNullOrWhiteSpace(selectedFiles))
            await rdClient.SelectTorrentFiles(torrentId, selectedFiles);

        return (torrentId, torrentSource);
    }
    

    [GeneratedRegex(@"^magnet:\?xt=urn:.*$")]
    private static partial Regex MagnetRegex();
}