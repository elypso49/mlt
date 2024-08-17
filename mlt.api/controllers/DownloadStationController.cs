namespace mlt.api.controllers;

[Route("[controller]"), ApiController]
public class DownloadStationController(IDownloadStationService service) : BaseController
{
    [HttpGet]
    public Task<ActionResult> GetTasks()
        => HandleRequest(async () =>
                         {
                             var result = await service.GetTasks();

                             return (result != null, Ok(result));
                         });

    [HttpGet("CreateTask")]
    public Task<ActionResult> CreateTask(string uri, string? destination)
        => HandleRequest(async () =>
                         {
                             await service.CreateTask(uri.Split(',', StringSplitOptions.RemoveEmptyEntries), destination);

                             return (true, Ok());
                         });
}