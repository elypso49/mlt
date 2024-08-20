namespace mlt.api.controllers;

[Route("[controller]"), ApiController]
public class RssFeedResultsController(IRssFeedResultService service, IRssFeedProcessorService rssFeedProcessorService) : CrudController<RssFeedResult>(service)
{
    [HttpPost("{rssFeedId}/fetch")]
    public Task<IActionResult> ProcessFeed(string rssFeedId)
        => HandleRequest(async () => await rssFeedProcessorService.ProcessFeed(rssFeedId) is { } processedFeed
            ? Ok(processedFeed)
            : NotFound(rssFeedId));
}