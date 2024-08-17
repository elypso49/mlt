using mlt.workflow.services;

namespace mlt.api.controllers;

[Route("[controller]"), ApiController]
public class WorkflowController(IWorkflowService service) : BaseController
{
    [HttpPost]
    public Task<ActionResult> DownloadAll()
        => HandleRequest(async () =>
                         {
                             var result = await service.DownloadAll();

                             return (result, Ok(result));
                         });
}