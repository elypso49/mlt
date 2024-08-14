namespace mlt.realdebrid.services;

public class RealDebridService(IRealDebridHttpClient rdClient) : IRealDebridService
{
    public Task<IEnumerable<RealDebridTorrentInfo>> GetDownloads()
        => rdClient.GetDownloads();

    public Task<IEnumerable<RealDebridTorrentInfo>> GetTorrents()
        => rdClient.GetTorrents();

    public async Task<IEnumerable<RealDebridTorrentInfo>> UnrestrictLinks(string[] links)
        => await Task.WhenAll(links.Select(rdClient.UnrestrictLink).ToList());

    public async Task<List<string>> AddMagnet(string[] magnets)
    {
        var result = new List<string>();

        foreach (var magnet in magnets)
        {
            var torrentId = await rdClient.AddMagnet(magnet);
            var torrent = await rdClient.GetTorrentInfo(torrentId);

            foreach (var file in torrent.Files)
            {
                var extension = Path.GetExtension(file.Path);
                file.Selected = extension == ".mkv";
            }

            var selectedFiles = string.Join(',', torrent.Files.Where(x => x.Selected == true).Select(x => x.Id));

            if (!string.IsNullOrWhiteSpace(selectedFiles))
                await rdClient.SelectTorrentFiles(torrentId, selectedFiles);

            result.Add(torrentId);
        }

        return result;
    }
    
    public async Task<List<string>> AddTorrentFile(string[] torrentFiles)
    {
        var result = new List<string>();

        foreach (var torrentFile in torrentFiles)
        {
            var torrentId = await rdClient.AddTorrent(torrentFile);
            var torrent = await rdClient.GetTorrentInfo(torrentId);

            foreach (var file in torrent.Files)
            {
                var extension = Path.GetExtension(file.Path);
                file.Selected = extension == ".mkv";
            }

            var selectedFiles = string.Join(',', torrent.Files.Where(x => x.Selected == true).Select(x => x.Id));

            if (!string.IsNullOrWhiteSpace(selectedFiles))
                await rdClient.SelectTorrentFiles(torrentId, selectedFiles);

            result.Add(torrentId);
        }

        return result;
    }
}