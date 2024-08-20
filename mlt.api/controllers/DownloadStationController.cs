namespace mlt.api.controllers;

[Route("[controller]"), ApiController]
public class DownloadStationController(IDownloadStationService service) : BaseController
{
    [HttpGet]
    public Task<IActionResult> GetTasks()
        => HandleRequest(async () => Ok(await service.GetTasks()));

    [HttpGet("CreateTask")]
    public Task<IActionResult> CreateTask(string uri, string? destination)
        => HandleRequest(async () => Ok(await service.CreateTask(uri.Split(',', StringSplitOptions.RemoveEmptyEntries), destination)));
}