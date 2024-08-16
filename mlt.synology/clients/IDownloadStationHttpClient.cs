using SynoTask = mlt.dtos.synology.SynoTask;

namespace mlt.synology.clients;

internal interface IDownloadStationHttpClient
{
    public Task<IEnumerable<SynoTask>> GetTasks();
    public Task<(string uri, bool isSuccess)> CreateTask(string uri, string destination);
}