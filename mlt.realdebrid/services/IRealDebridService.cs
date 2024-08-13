namespace mlt.realdebrid.services;

public interface IRealDebridService
{
    public Task<IEnumerable<RdFileInfo>> GetDownloads();
}