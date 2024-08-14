namespace mlt.realdebrid.services;

public interface IRealDebridService
{
    public Task<IEnumerable<RealDebridTorrentInfo>> GetDownloads();
    public Task<IEnumerable<RealDebridTorrentInfo>> GetTorrents();
    public Task<IEnumerable<RealDebridTorrentInfo>> UnrestrictLinks(string[] links);
    public Task<List<string>> AddMagnet(string[] magnets);
    public Task<List<string>> AddTorrentFile(string[] torrentFiles);
}