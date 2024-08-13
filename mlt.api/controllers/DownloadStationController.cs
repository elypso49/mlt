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
}