using Microsoft.AspNetCore.Mvc;
using mlt.common.domainEntities;
using mlt.common.services;

namespace mlt.api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RssFeedResultsController(IRssFeedResultService rssFeedResultService, IRssFeedProcessorService rssFeedProcessorService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RssFeedResult>>> GetRssFeedResults()
        => Ok(await rssFeedResultService.GetAll());

    [HttpGet("{id}")]
    public async Task<ActionResult<RssFeedResult>> GetRssFeedResult(string id)
    {
        var result = await rssFeedResultService.GetById(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("{rssFeedId}/fetch")]
    public async Task<IActionResult> ProcessFeed(string rssFeedId)
    {
        try
        {
            await rssFeedProcessorService.ProcessFeed(rssFeedId);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutRssFeedResult(string id, RssFeedResult result)
    {
        var existingResult = rssFeedResultService.GetById(id);

        if (existingResult == null)
            return NotFound();

        await rssFeedResultService.Update(id, result);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRssFeedResult(string id)
    {
        var existingResult = rssFeedResultService.GetById(id);

        if (existingResult == null)
            return NotFound();

        await rssFeedResultService.Delete(id);

        return NoContent();
    }
}