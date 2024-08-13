namespace mlt.realdebrid.services;

public class RealDebridService(IRealDebridHttpClient rdClient) : IRealDebridService
{
    public Task<IEnumerable<RdFileInfo>> GetDownloads() => rdClient.GetDownloads();

    // public async Task<IEnumerable<bool>> GetTasks()
    //     => await rdClientGet .GetTasks();
}