using mlt.common.dtos.realdebrid;

namespace mlt.realdebrid.clients;

internal interface IRealDebridHttpClient
{
    public Task<IEnumerable<RealDebridTorrentInfo>> GetDownloads();
    public Task<IEnumerable<RealDebridTorrentInfo>> GetTorrents();
    public Task<RealDebridTorrentInfo> UnrestrictLink(string link);
    public Task<string> AddMagnet(string magnet);
    public Task<string> AddTorrent(string filePath);
    public Task<RealDebridTorrentInfo> GetTorrentInfo(string id);
    public Task SelectTorrentFiles(string torrentId, string selectedFiles);
}