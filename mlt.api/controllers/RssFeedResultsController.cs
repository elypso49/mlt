using Microsoft.AspNetCore.Mvc;
using mlt.common.controllers;
using mlt.common.dtos.rss;
using mlt.rss.services;

namespace mlt.api.controllers;

[Route("api/[controller]"), ApiController]
public class RssFeedResultsController(IRssFeedResultService service, IRssFeedProcessorService rssFeedProcessorService) : CrudController<RssFeedResult>(service)
{
    [HttpPost("{rssFeedId}/fetch")]
    public Task<IActionResult> ProcessFeed(string rssFeedId)
        => HandleRequest(async () => await rssFeedProcessorService.ProcessFeed(rssFeedId) is { } processedFeed
            ? Ok(processedFeed)
            : NotFound(rssFeedId));
}