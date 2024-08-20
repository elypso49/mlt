using mlt.common.dtos.responses;
using mlt.common.dtos.synology;
using mlt.common.services;
using mlt.synology.clients;

namespace mlt.synology.services;

internal class DownloadStationService(IDownloadStationHttpClient dsClient) : BaseService, IDownloadStationService
{
    public Task<ResponseDto<IEnumerable<SynoTask>>> GetTasks()
        => HandleDataRetrievement(async () => await dsClient.GetTasks());

    public async Task<ResponseDto<List<SynoCreateTaskResponse>>?> CreateTask(IEnumerable<string> uri, string destination = "Movies")
    {
        var result = new ResponseDto<List<SynoCreateTaskResponse>> { Data = [] };

        try
        {
            foreach (var uriItem in uri)
                result.Data.Add(await dsClient.CreateTask(uriItem, destination!));
        }
        catch (Exception e)
        {
            return ManageError<List<SynoCreateTaskResponse>>(e);
        }

        return result;
    }
}