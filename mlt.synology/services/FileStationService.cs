namespace mlt.synology.services;

public class FileStationService(IFileStationHttpClient client) : IFileStationService 
{
    public Task<List<SynoFolder>> GetFoldersWithSubs()
        => client.GetFoldersWithSubs();
}