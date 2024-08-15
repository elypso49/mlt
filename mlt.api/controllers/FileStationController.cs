namespace mlt.api.controllers;

[Route("[controller]"), ApiController]
public class FileStationController(IFileStationService service) : BaseController
{
    [HttpGet]
    public Task<ActionResult> GetTasks()
        => HandleRequest(async () =>
                         {
                             var result = await service.GetFoldersWithSubs();

                             return (result != null, Ok(result));
                         });
}