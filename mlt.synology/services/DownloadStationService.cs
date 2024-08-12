using mlt.synology.datas;
using mlt.synology.dtos;

namespace mlt.synology.services;

public class DownloadStationService(IDownloadStationHttpClient dsClient) : IDownloadStationService
{
    public async Task<IEnumerable<SynoTask>> GetTasks()
        => await dsClient.GetTasks();
}