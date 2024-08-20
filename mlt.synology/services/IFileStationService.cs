using mlt.common.dtos.responses;
using mlt.common.dtos.synology;

namespace mlt.synology.services;

public interface IFileStationService
{
    public Task<ResponseDto<List<SynoFolder>>> GetFoldersWithSubs();
}