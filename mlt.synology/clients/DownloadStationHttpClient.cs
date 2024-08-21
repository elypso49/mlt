using AutoMapper;
using Microsoft.Extensions.Options;
using mlt.common.dtos.synology;
using mlt.common.extensions;
using mlt.common.options;
using mlt.synology.clients.dtos;

namespace mlt.synology.clients;

internal class DownloadStationHttpClient(
    IOptions<SynologyOptions> synologyOptions,
    IMapper mapper,
    IFileStationHttpClient fileStationHttpClient) : SynologyHttpClient(synologyOptions.Value, "webapi/DownloadStation/task.cgi"), IDownloadStationHttpClient
{
    private const string TaskApi = "SYNO.DownloadStation.Task";

    public async Task<IEnumerable<SynoTask>> GetTasks()
    {
        var response = await GetSynoAsync<SynoResponse>(TaskApi, "1", "list", "&additional=detail,transfer,file,tracker,peer");

        var synoTasks = mapper.Map<IEnumerable<SynoTask>>(response.Data.Tasks);

        return synoTasks;
    }

    public async Task<SynoCreateTaskResponse> CreateTask(string uri, string destination)
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

        return new() { Uri = uri, IsSuccess = response.Success };
    }
}