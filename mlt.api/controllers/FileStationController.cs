using Microsoft.AspNetCore.Mvc;
using mlt.common.controllers;
using mlt.synology.services;

namespace mlt.api.controllers;

[Route("api/[controller]"), ApiController]
public class FileStationController(IFileStationService service) : BaseController
{
    [HttpGet]
    public Task<IActionResult> Get()
        => HandleRequest(async () => Ok(await service.GetFoldersWithSubs()));
}