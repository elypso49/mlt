namespace mlt.synology.clients;

public class DownloadStationHttpClient(JsonSerializerOptions jsonSerializerOptions, IOptions<SynologyOptions> options, IMapper mapper)
    : SynologyHttpClient(jsonSerializerOptions, options), IDownloadStationHttpClient
{
    protected override string ParamApi => "&api=SYNO.DownloadStation.Task&version=1";

    public async Task<IEnumerable<SynoTask>> GetTasks()
    {
        var response = await GetAsync<SynoResponse>($"{BaseDsApi}&method=list&additional=detail,transfer,file,tracker,peer");

        var synoTasks = mapper.Map<IEnumerable<SynoTask>>(response.Data.Tasks);

        return synoTasks;
    }
}