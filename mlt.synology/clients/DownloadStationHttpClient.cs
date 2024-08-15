namespace mlt.synology.clients;

internal class DownloadStationHttpClient(JsonSerializerOptions jsonSerializerOptions, IOptions<SynologyOptions> options, IMapper mapper)
    : SynologyHttpClient(jsonSerializerOptions, options), IDownloadStationHttpClient
{
    protected override string ParamApi => "&api=SYNO.DownloadStation.Task&version=1";

    public async Task<IEnumerable<SynoTask>> GetTasks()
    {
        var response = await GetAsync<SynoResponse>($"{BaseDsApi}&method=list&additional=detail,transfer,file,tracker,peer");

        var synoTasks = mapper.Map<IEnumerable<SynoTask>>(response.Data.Tasks);

        return synoTasks;
    }

    public async Task CreateTask(string uri, string destination)
    {
        var response = await GetAsync<SynoResponse>($"{BaseDsApi}&method=create&uri={uri}&destination={destination}");

        if (!response.Success)
            throw new Exception("Error while trying to create the task");
    }
}