namespace mlt.synology.clients;

public interface IFileStationHttpClient
{
    public Task<List<SynoFolder>> GetFoldersWithSubs(string? folderPath = null);
}