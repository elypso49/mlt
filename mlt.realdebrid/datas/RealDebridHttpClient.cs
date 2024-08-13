namespace mlt.realdebrid.datas;

public class RealDebridHttpClient : HttpService, IRealDebridHttpClient
{
    private readonly IMapper _mapper;

    public RealDebridHttpClient(JsonSerializerOptions jsonSerializerOptions, IOptions<RealDebridOptions> options, IMapper mapper) : base(jsonSerializerOptions)
    {
        _mapper = mapper;
        SetBearerToken(options.Value.ApiToken);
    }

    private static string BaseUrl => "https://api.real-debrid.com/rest/1.0/";

    public async Task<IEnumerable<RdFileInfo>> GetDownloads()
    {
        var result = await GetAsync<IEnumerable<DownloadItem>>($"{BaseUrl}downloads");
        return _mapper.Map<IEnumerable<RdFileInfo>>(result);
    }

    public async Task<DownloadItem> GetTorrents()
        => await GetAsync<DownloadItem>($"{BaseUrl}torrents");
}