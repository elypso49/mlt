using Microsoft.AspNetCore.Mvc;
using mlt.common.domainEntities.RssFeed;
using mlt.services.RssFeed;

namespace mlt.api.Controllers;

[Route("[controller]")]
[ApiController]
public class RssFeedResultsController(IRssFeedResultService rssFeedResultService, IRssFeedProcessorService rssFeedProcessorService) : BaseController
{
    [HttpGet]
    public Task<ActionResult> GetRssFeedResults()
        => HandleRequest(async () => (true, Ok(await rssFeedResultService.GetAll())));

    [HttpGet("{id}")]
    public Task<ActionResult> GetRssFeedResult(string id)
        => HandleRequest(async () =>
                         {
                             var result = await rssFeedResultService.GetById(id);
                             return (result != null, Ok(result));
                         });
    
    [HttpPost("{rssFeedId}/fetch")]
    public Task<ActionResult> ProcessFeed(string rssFeedId)
        => HandleRequest(async () =>
                         {
                             await rssFeedProcessorService.ProcessFeed(rssFeedId);
                             return (true, Ok());
                         });
    
    [HttpPut("{id}")]
    public Task<ActionResult> PutRssFeedResult(string id, RssFeedResult result)
        => HandleRequest(async () =>
                         {
                             if (!await CheckIfExists(id))
                                 return (false, null)!;

                             await rssFeedResultService.Update(id, result);

                             return (true, NoContent());
                         });

    private async Task<bool> CheckIfExists(string id)
        => await rssFeedResultService.GetById(id) != null;

    [HttpDelete("{id}")]
    public Task<ActionResult> DeleteRssFeedResult(string id)
        => HandleRequest(async () =>
                         {
                             if (!await CheckIfExists(id))
                                 return (false, null!);

                             await rssFeedResultService.Delete(id);

                             return (true, NoContent());
                         });
}