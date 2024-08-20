namespace mlt.api.controllers;

[Route("[controller]"), ApiController]
public class FileStationController(IFileStationService service) : BaseController
{
    [HttpGet]
    public Task<IActionResult> GetTasks()
        => HandleRequest(async () => Ok(await service.GetFoldersWithSubs()));
}