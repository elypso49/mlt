using mlt.common.dtos.synology;

namespace mlt.synology.clients;

internal interface IDownloadStationHttpClient
{
    public Task<IEnumerable<SynoTask>> GetTasks();
    public Task<SynoCreateTaskResponse> CreateTask(string uri, string destination);
}