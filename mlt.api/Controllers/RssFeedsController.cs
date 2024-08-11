using Microsoft.AspNetCore.Mvc;
using mlt.common.domainEntities;
using mlt.services.RssFeed;

namespace mlt.api.Controllers;

[Route("[controller]")]
[ApiController]
public class RssFeedsController(IRssFeedService service, IRssFeedResultService rssFeedResultService) : CrudController<RssFeed>(service)
{
    [HttpGet("{id}/Results")]
    [ProducesResponseType(typeof(IEnumerable<RssFeed>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<ActionResult> GetRssFeedResultByRssFeedId(string id)
        => HandleRequest(async () =>
                         {
                             var result = await rssFeedResultService.GetByRssFeedId(id);
                             return (result != null, Ok(result));
                         });
    
    // [HttpGet]
    // [ProducesResponseType(typeof(IEnumerable<RssFeed>), StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    // public Task<ActionResult> GetRssFeeds()
    // => HandleRequest(async () => (true, Ok(await service.GetAll())));
    //
    // [HttpGet("{id}")]
    // [ProducesResponseType(typeof(RssFeed), StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    // public Task<ActionResult> GetRssFeed(string id)
    //     => HandleRequest(async () =>
    //                      {
    //                          var result = await service.GetById(id);
    //                          return (result != null, Ok(result));
    //                      });
    //
    
    //
    // [HttpPut("{id}")]
    // public async Task<ActionResult> PutRssFeed(string id, RssFeed feed)
    // {
    //     var existingFeed = await service.GetById(id);
    //
    //     if (existingFeed == null)
    //         return NotFound();
    //
    //     await service.Update(id, feed);
    //
    //     return NoContent();
    // }
    //
    // [HttpDelete("{id}")]
    // public async Task<IActionResult> DeleteRssFeed(string id)
    // {
    //     var existingFeed = service.GetById(id);
    //
    //     if (existingFeed == null)
    //         return NotFound();
    //
    //     await service.Delete(id);
    //
    //     return NoContent();
    // }
}