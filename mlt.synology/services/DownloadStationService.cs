namespace mlt.synology.services;

public class DownloadStationService(IDownloadStationHttpClient dsClient) : IDownloadStationService
{
    public async Task<IEnumerable<SynoTask>> GetTasks() => await dsClient.GetTasks();
}