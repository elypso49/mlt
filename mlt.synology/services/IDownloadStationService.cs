using mlt.common.dtos.responses;
using mlt.common.dtos.synology;
using SynoTask = mlt.common.dtos.synology.SynoTask;

namespace mlt.synology.services;

public interface IDownloadStationService
{
    public Task<ResponseDto<IEnumerable<SynoTask>>> GetTasks();
    public Task<ResponseDto<IEnumerable<SynoTask>>> CleanTasks();
    public Task<ResponseDto<List<SynoCreateTaskResponse>>?> CreateTask(IEnumerable<string> uri, string destination = "Movies");
}