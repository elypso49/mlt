using Microsoft.AspNetCore.Mvc;
using mlt.common.controllers;
using mlt.realdebrid.services;

namespace mlt.api.controllers;

[Route("api/[controller]"), ApiController]
public class RealDebridController(IRealDebridService service) : BaseController
{
    [HttpGet("/downloads")]
    public Task<IActionResult> GetDownloads()
        => HandleRequest(async () => Ok(await service.GetDownloads()));

    [HttpGet("/torrents")]
    public Task<IActionResult> GetTorrents()
        => HandleRequest(async () => Ok(await service.GetTorrents()));

    [HttpPost("/unrestrict")]
    public Task<IActionResult> UnrestrictLinks(string[] links)
        => HandleRequest(async () => Ok(await service.UnrestrictLinks(links)));

    [HttpPost("/addtorrent")]
    public Task<IActionResult> AddTorrentFiles(string[] links)
        => HandleRequest(async () => Ok(await service.AddTorrentsInBatchesWithRetry(links)));
}