namespace mlt.api.controllers;

[Route("[controller]"), ApiController]
public class RealDebridController(IRealDebridService service) : BaseController
{
    [HttpGet("/downloads")]
    public Task<IActionResult> GetDownloads()
        => HandleRequest(async () => Ok(await service.GetDownloads()));

    [HttpGet("/torrents")]
    public Task<IActionResult> GetTorrents()
        => HandleRequest(async () => Ok(await service.GetTorrents()));

    [HttpPost("/unrestrict")]
    public Task<IActionResult> PostUnrestrict(string[] links)
        => HandleRequest(async () => Ok(await service.UnrestrictLinks(links)));

    [HttpPost("/addtorrent")]
    public Task<IActionResult> AddTorrentFile(string[] links)
        => HandleRequest(async () => Ok(await service.AddTorrentsInBatchesWithRetry(links)));
}