using System.Collections;

namespace mlt.realdebrid.services;

public interface IRealDebridService
{
    public Task<IEnumerable<RealDebridTorrentInfo>> GetDownloads();
    public Task<IEnumerable<RealDebridTorrentInfo>> GetTorrents();
    public Task<IEnumerable<RealDebridTorrentInfo>> UnrestrictLinks(IEnumerable<string> links);
    public Task<IEnumerable<string>> AddTorrentsInBatchesWithRetry(IEnumerable<string> torrentLinks, int batchSize = 2, int initialDelayMilliseconds = 1000, int maxRetries = 3);
    public Task<RealDebridTorrentInfo> GetTorrentInfo(string torrentId);
}