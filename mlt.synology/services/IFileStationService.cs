namespace mlt.synology.services;

public interface IFileStationService
{
    public Task<List<SynoFolder>> GetFoldersWithSubs();
}