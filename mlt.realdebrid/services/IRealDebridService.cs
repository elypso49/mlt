using mlt.common.dtos.realdebrid;
using mlt.common.dtos.responses;

namespace mlt.realdebrid.services;

public interface IRealDebridService
{
    public Task<ResponseDto<IEnumerable<RealDebridTorrentInfo>>> GetDownloads();
    public Task<ResponseDto<IEnumerable<RealDebridTorrentInfo>>> GetTorrents();
    public Task<ResponseDto<IEnumerable<RealDebridTorrentInfo>>> UnrestrictLinks(IEnumerable<string> links);
    public Task<ResponseDto<RealDebridTorrentInfo>> GetTorrentInfo(string torrentId);

    public Task<ResponseDto<List<AddTorrentResponse>>> AddTorrentsInBatchesWithRetry(
        IEnumerable<string> torrentLinks,
        int batchSize = 2,
        int initialDelayMilliseconds = 1000,
        int maxRetries = 3,
        int maxConcurent = 30);
}