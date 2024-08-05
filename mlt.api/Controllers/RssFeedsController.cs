using Microsoft.AspNetCore.Mvc;
using mlt.common.domainEntities.RssFeed;
using mlt.services.RssFeed;

namespace mlt.api.Controllers;

[Route("[controller]")]
[ApiController]
public class RssFeedsController(IRssFeedService service) : BaseController
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<RssFeed>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetRssFeeds()
        => Ok(await service.GetAll());

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(RssFeed), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetRssFeed(string id)
    {
        var feed = await service.GetById(id);

        if (feed == null)
            return NotFound();

        return Ok(feed);
    }

    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<RssFeed>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> PostRssFeed(RssFeed feed)
    {
        await service.Add(feed);

        return CreatedAtAction(nameof(GetRssFeed), new { id = feed.Id }, feed);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> PutRssFeed(string id, RssFeed feed)
    {
        var existingFeed = await service.GetById(id);

        if (existingFeed == null)
            return NotFound();

        await service.Update(id, feed);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRssFeed(string id)
    {
        var existingFeed = service.GetById(id);

        if (existingFeed == null)
            return NotFound();

        await service.Delete(id);

        return NoContent();
    }
}