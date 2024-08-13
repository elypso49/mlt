namespace mlt.realdebrid.datas;

public interface IRealDebridHttpClient
{
    public Task<IEnumerable<RdFileInfo>> GetDownloads();
    public Task<DownloadItem> GetTorrents();
}