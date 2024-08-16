using mlt.dtos.rss.enums;

namespace mlt.api.controllers;

[Route("[controller]"), ApiController]
public class RssFeedsController(IRssFeedService service, IRssFeedResultService rssFeedResultService) : CrudController<RssFeed>(service)
{
    [HttpGet("{id}/results"), ProducesResponseType(typeof(IEnumerable<RssFeed>), StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<ActionResult> GetRssFeedResultByRssFeedId(string id)
        => HandleRequest(async () =>
                         {
                             var result = await rssFeedResultService.GetByRssFeedId(id);

                             return (result != null, Ok(result));
                         });

    [HttpGet("bystatus"), ProducesResponseType(typeof(IEnumerable<RssFeed>), StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<ActionResult> GetRssFeedResultByRssFeedId(StateValue stateValue)
        => HandleRequest(async () =>
                         {
                             var result = await rssFeedResultService.GetByStatus(stateValue);

                             return (result != null, Ok(result));
                         });
}