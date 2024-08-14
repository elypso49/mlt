namespace mlt.synology.clients;

public interface IDownloadStationHttpClient
{
    public Task<IEnumerable<SynoTask>> GetTasks();
}