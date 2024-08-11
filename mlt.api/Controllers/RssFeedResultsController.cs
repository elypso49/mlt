using Microsoft.AspNetCore.Mvc;
using mlt.common.domainEntities;
using mlt.services.RssFeed;

namespace mlt.api.Controllers;

[Route("[controller]")]
[ApiController]
public class RssFeedResultsController(IRssFeedResultService service, IRssFeedProcessorService rssFeedProcessorService) : CrudController<RssFeedResult>(service)
{
    [HttpPost("{rssFeedId}/fetch")]
    public Task<ActionResult> ProcessFeed(string rssFeedId)
        => HandleRequest(async () =>
                         {
                             await rssFeedProcessorService.ProcessFeed(rssFeedId);
                             return (true, Ok());
                         });
}