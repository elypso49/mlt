namespace mlt.common.controllers;

public abstract class CrudController<T>(ICrudService<T> service) : BaseController
    where T : Identifiable
{
    [HttpGet]
    public Task<ActionResult> GetAll() => HandleRequest(async () => (true, Ok(await service.GetAll())));

    [HttpGet("{id}")]
    public Task<ActionResult> GetById(string id)
        => HandleRequest(async () =>
                         {
                             var result = await service.GetById(id);

                             return (result != null, Ok(result));
                         });

    [HttpPost]
    public async Task<ActionResult> PostRssFeed(T feed)
    {
        var created = await service.Add(feed);

        return CreatedAtAction(nameof(PostRssFeed), created);
    }

    [HttpPut("{id}")]
    public Task<ActionResult> Put(string id, T result)
        => HandleRequest(async () =>
                         {
                             if (!await CheckIfExists(id))
                                 return (false, null)!;

                             await service.Update(id, result);

                             return (true, NoContent());
                         });

    private async Task<bool> CheckIfExists(string id) => await service.GetById(id) != null;

    [HttpDelete("{id}")]
    public Task<ActionResult> Delete(string id)
        => HandleRequest(async () =>
                         {
                             if (!await CheckIfExists(id))
                                 return (false, null!);

                             await service.Delete(id);

                             return (true, NoContent());
                         });
}