using mlt.workflow.services;

namespace mlt.api.controllers;

[Route("[controller]"), ApiController]
public class WorkflowController(IWorkflowService service) : BaseController
{
    [HttpGet]
    public Task<ActionResult> GetTasks()
        => HandleRequest(async () =>
                         {
                             var result = await service.DownloadAll();

                             return (result, Ok(result));
                         });
}