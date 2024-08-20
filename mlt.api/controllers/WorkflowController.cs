using Microsoft.AspNetCore.Mvc;
using mlt.common.controllers;
using mlt.workflow.services;

namespace mlt.api.controllers;

[Route("api/[controller]"), ApiController]
public class WorkflowController(IWorkflowService service) : BaseController
{
    [HttpPost]
    public Task<IActionResult> DownloadAll()
        => HandleRequest(async () => Ok(await service.DownloadAll()));
}