using mlt.workflow.services;

namespace mlt.api.controllers;

[Route("[controller]"), ApiController]
public class WorkflowController(IWorkflowService service) : BaseController
{
    [HttpPost]
    public Task<IActionResult> DownloadAll()
        => HandleRequest(async () => Ok(await service.DownloadAll()));
}