using mlt.common.dtos.responses;
using mlt.common.dtos.synology;
using mlt.common.services;
using mlt.synology.clients;

namespace mlt.synology.services;

public class FileStationService(IFileStationHttpClient client) : BaseService, IFileStationService
{
    public Task<ResponseDto<List<SynoFolder>>> GetFoldersWithSubs()
        => HandleDataRetrievement(async () => await client.GetFoldersWithSubs());
}