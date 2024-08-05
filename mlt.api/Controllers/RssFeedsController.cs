using Microsoft.AspNetCore.Mvc;
using mlt.common.domainEntities;
using mlt.common.services;

namespace mlt.api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RssFeedsController(IRssFeedService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RssFeed>>> GetRssFeeds()
        => Ok(await service.GetAll());

    [HttpGet("{id}")]
    public async Task<ActionResult<RssFeed>> GetRssFeed(string id)
    {
        var feed = await service.GetById(id);

        if (feed == null)
            return NotFound();

        return Ok(feed);
    }

    [HttpPost]
    public async Task<ActionResult<RssFeed>> PostRssFeed(RssFeed feed)
    {
        await service.Add(feed);

        return CreatedAtAction(nameof(GetRssFeed), new { id = feed.Id }, feed);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutRssFeed(string id, RssFeed feed)
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