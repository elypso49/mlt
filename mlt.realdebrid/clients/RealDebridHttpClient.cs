namespace mlt.realdebrid.clients;

internal class RealDebridHttpClient(JsonSerializerOptions jsonSerializerOptions, IOptions<RealDebridOptions> options, IMapper mapper)
    : HttpService(jsonSerializerOptions, options.Value.BaseUrl, options.Value.ApiToken), IRealDebridHttpClient
{
    public async Task<IEnumerable<RealDebridTorrentInfo>> GetDownloads()
        => mapper.Map<IEnumerable<RealDebridTorrentInfo>>(await GetAsync<IEnumerable<DownloadResponse>>("downloads"));

    public async Task<IEnumerable<RealDebridTorrentInfo>> GetTorrents()
        => mapper.Map<IEnumerable<RealDebridTorrentInfo>>(await GetAsync<IEnumerable<TorrentModel>>("torrents"));

    public async Task<RealDebridTorrentInfo> UnrestrictLink(string link)
        => mapper.Map<RealDebridTorrentInfo>(await PostAsync<DownloadResponse>("unrestrict/link", new Dictionary<string, string> { { "link", link } }));

    public async Task<string> AddMagnet(string magnet)
        => (await PostAsync<AddTorentResponse>("torrents/addMagnet", new Dictionary<string, string> { { "magnet", magnet } }))!.Id;

    public async Task<string> AddTorrent(string filePath)
        => (await PutAsync<AddTorentResponse>("torrents/addTorrent", filePath))!.Id;

    public async Task<RealDebridTorrentInfo> GetTorrentInfo(string id)
        => mapper.Map<RealDebridTorrentInfo>(await GetAsync<TorrentModel>($"torrents/info/{id}"));

    public Task SelectTorrentFiles(string torrentId, string selectedFiles)
        => PostAsync<AddTorentResponse>($"torrents/selectFiles/{torrentId}", new Dictionary<string, string> { { "files", selectedFiles } });
}