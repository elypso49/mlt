using mlt.common.extensions;

namespace mlt.synology.clients;

internal class DownloadStationHttpClient(
    JsonSerializerOptions jsonSerializerOptions,
    IOptions<SynologyOptions> synologyOptions,
    IMapper mapper,
    IFileStationHttpClient fileStationHttpClient) : SynologyHttpClient(jsonSerializerOptions, synologyOptions.Value, "webapi/DownloadStation/task.cgi"), IDownloadStationHttpClient
{
    // protected override string ParamApi => "&api=SYNO.DownloadStation.Task&version=1";
    private const string TaskApi = "SYNO.DownloadStation.Task";

    public async Task<IEnumerable<SynoTask>> GetTasks()
    {
        var response = await GetSynoAsync<SynoResponse>(TaskApi, "1", "list", "&additional=detail,transfer,file,tracker,peer");

        var synoTasks = mapper.Map<IEnumerable<SynoTask>>(response.Data.Tasks);

        return synoTasks;
    }

    public async Task<(string uri, bool isSuccess)> CreateTask(string uri, string destination)
    {
        var response = await GetSynoAsync<SynoResponse>(TaskApi, "3", "create", $"&uri={uri}&destination={destination.ToUrlProof()}");

        if (!response.Success)
        {
            var destinationDetails = destination.Split('/');
            var folder = $"/{destinationDetails[0]}";

            for (var i = 1; i < destinationDetails.Length; i++)
            {
                await fileStationHttpClient.CreateFolder(folder, destinationDetails[i]);

                folder += $"/{destinationDetails[i]}";
            }

            response = await GetSynoAsync<SynoResponse>(TaskApi, "3", "create", $"&uri={uri}&destination={destination.ToUrlProof()}");

            if (!response.Success)
                throw new Exception($"Unable to create task {uri} for destination {destination}");
        }
        
        return (uri, response.Success);
    }
}