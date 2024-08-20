using Microsoft.AspNetCore.Mvc;
using mlt.common.services;

namespace mlt.common.controllers;

public abstract class CrudController<T>(ICrudService<T> service) : BaseController
    where T : Identifiable
{
    [HttpGet]
    public Task<IActionResult> GetAll()
        => HandleRequest(async () => Ok(await service.GetAll()));

    [HttpGet("{id}")]
    public Task<IActionResult> GetById(string id)
        => HandleRequest(async () => await CheckIfExists(id) is { } found ? Ok(found) : NotFound());

    [HttpPost]
    public Task<IActionResult> PostRssFeed(T feed)
        => HandleRequest(async () => CreatedAtAction(nameof(PostRssFeed), await service.Add(feed)));

    [HttpPut("{id}")]
    public Task<IActionResult> Put(string id, T result)
        => HandleRequest(async () => await CheckIfExists(id) is not null ? Ok(await service.Update(id, result)) : NotFound());

    [HttpDelete("{id}")]
    public Task<IActionResult> Delete(string id)
        => HandleRequest(async () => await CheckIfExists(id) is not null ? Ok(await service.Delete(id)) : NotFound());

    private async Task<T?> CheckIfExists(string id)
        => (await service.GetById(id)).Data;
}