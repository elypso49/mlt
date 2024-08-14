namespace mlt.api.controllers;

[Route("[controller]"), ApiController]
public class RealDebridController(IRealDebridService service) : BaseController
{
    [HttpGet("/downloads")]
    public Task<ActionResult> GetDownloads()
        => HandleRequest(async () =>
                         {
                             var result = await service.GetDownloads();

                             return (result != null, Ok(result));
                         });

    [HttpGet("/torrents")]
    public Task<ActionResult> GetTorrents()
        => HandleRequest(async () =>
                         {
                             var result = await service.GetTorrents();

                             return (result != null, Ok(result));
                         });

    [HttpPost("/unrestrict")]
    public Task<ActionResult> PostUnrestrict(string[] links)
        => HandleRequest(async () =>
                         {
                             var result = await service.UnrestrictLinks(links);

                             return (result != null, Ok(result));
                         });

    [HttpPost("/addmagnet")]
    public Task<ActionResult> PostAddMagnet(string[] links)
        => HandleRequest(async () =>
                         {
                             var result = await service.AddMagnet(links);

                             return (result != null, Ok(result));
                         });
    
    [HttpPost("/addtorrent")]
    public Task<ActionResult> AddTorrentFile(string[] links)
        => HandleRequest(async () =>
                         {
                             var result = await service.AddTorrentFile(links);

                             return (result != null, Ok(result));
                         });
}