using SynoTask = mlt.dtos.synology.SynoTask;

namespace mlt.synology.services;

public interface IDownloadStationService
{
    public Task<IEnumerable<SynoTask>> GetTasks();
    public Task CreateTask(IEnumerable<string> uri, string destination = "Movies");
}