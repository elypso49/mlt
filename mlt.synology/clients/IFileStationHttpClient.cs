using mlt.common.dtos.synology;

namespace mlt.synology.clients;

public interface IFileStationHttpClient
{
    public Task<List<SynoFolder>> GetFoldersWithSubs(string? folderPath = null, int level = 0);
    public Task<bool> CreateFolder(string folderPath, string folderName);
}