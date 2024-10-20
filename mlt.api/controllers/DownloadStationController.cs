using Microsoft.AspNetCore.Mvc;
using mlt.common.controllers;
using mlt.synology.services;

namespace mlt.api.controllers;

[Route("api/[controller]"), ApiController]
public class DownloadStationController(IDownloadStationService service) : BaseController
{
    [HttpGet]
    public Task<IActionResult> Get()
        => HandleRequest(async () => Ok(await service.GetTasks()));
    
    [HttpGet("Clean")]
    public Task<IActionResult> Clean()
        => HandleRequest(async () => Ok(await service.CleanTasks()));

    [HttpGet("CreateTask")]
    public Task<IActionResult> CreateTask(string uri, string? destination)
        => HandleRequest(async () => Ok(await service.CreateTask(uri.Split(',', StringSplitOptions.RemoveEmptyEntries), destination)));
}