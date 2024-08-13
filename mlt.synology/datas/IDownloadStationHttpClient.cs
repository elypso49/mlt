namespace mlt.synology.datas;

public interface IDownloadStationHttpClient
{
    public Task<IEnumerable<SynoTask>> GetTasks();
}