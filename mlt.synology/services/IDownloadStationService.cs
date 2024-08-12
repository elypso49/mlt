using mlt.synology.dtos;

namespace mlt.synology.services;

public interface IDownloadStationService
{
    public Task<IEnumerable<SynoTask>> GetTasks();
}