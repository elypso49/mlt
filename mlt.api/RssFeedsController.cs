﻿using Microsoft.AspNetCore.Mvc;
using mlt.common.controllers;
using mlt.rss.dtos;
using mlt.rss.services;

namespace mlt.api;

[Route("[controller]")]
[ApiController]
public class RssFeedsController(IRssFeedService service, IRssFeedResultService rssFeedResultService) : CrudController<RssFeed>(service)
{
    [HttpGet("{id}/results")]
    [ProducesResponseType(typeof(IEnumerable<RssFeed>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<ActionResult> GetRssFeedResultByRssFeedId(string id)
        => HandleRequest(async () =>
                         {
                             var result = await rssFeedResultService.GetByRssFeedId(id);

                             return (result != null, Ok(result));
                         });
}