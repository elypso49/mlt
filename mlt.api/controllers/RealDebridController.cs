using Microsoft.AspNetCore.Mvc;
using mlt.common.controllers;
using mlt.realdebrid.services;

namespace mlt.api.controllers;

[Route("[controller]")]
[ApiController]
public class RealDebridController(IRealDebridService service) : BaseController
{
    [HttpGet]
    public Task<ActionResult> GetTasks()
        => HandleRequest(async () =>
                         {
                             var result = await service.GetDownloads();

                             return (result != null, Ok(result));
                         });
}