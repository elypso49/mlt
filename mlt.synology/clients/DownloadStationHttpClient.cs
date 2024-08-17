using mlt.common.extensions;

namespace mlt.synology.clients;

internal class DownloadStationHttpClient(
    JsonSerializerOptions jsonSerializerOptions,
    IOptions<SynologyOptions> synologyOptions,
    IMapper mapper,
    IFileStationHttpClient fileStationHttpClient) : SynologyHttpClient(jsonSerializerOptions, synologyOptions.Value, "webapi/DownloadStation/task.cgi"), IDownloadStationHttpClient
{
    private const string TaskApi = "SYNO.DownloadStation.Task";

    public async Task<IEnumerable<SynoTask>> GetTasks()
    {
        var response = await GetSynoAsync<SynoResponse>(TaskApi, "1", "list", "&additional=detail,transfer,file,tracker,peer");

        var synoTasks = mapper.Map<IEnumerable<SynoTask>>(response.Data.Tasks);

        return synoTasks;
    }

    public async Task<(string uri, bool isSuccess)> CreateTask(string uri, string destination)
    {
        var safeUri = uri.ToUrlSafeString();
        var safeDestination = destination.RemoveUnsafeFolderCharacters();

        var response = await GetSynoAsync<SynoResponse>(TaskApi, "3", "create", $"&uri={safeUri}&destination={safeDestination}");

        if (!response.Success)
        {
            var destinationDetails = safeDestination.Split('/');
            var folder = $"/{destinationDetails[0]}";

            for (var i = 1; i < destinationDetails.Length; i++)
            {
                await fileStationHttpClient.CreateFolder(folder, destinationDetails[i]);

                folder += $"/{destinationDetails[i]}";
            }

            response = await GetSynoAsync<SynoResponse>(TaskApi, "3", "create", $"&uri={safeUri}&destination={safeDestination}");

            if (!response.Success)
                throw new Exception($"Unable to create task {safeUri} for destination {safeDestination}");
        }

        return (uri, response.Success);
    }
}