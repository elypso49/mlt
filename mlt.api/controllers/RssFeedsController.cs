﻿using Microsoft.AspNetCore.Mvc;
using mlt.common.controllers;
using mlt.common.dtos.rss;
using mlt.common.dtos.rss.enums;
using mlt.rss.services;

namespace mlt.api.controllers;

[Route("api/[controller]"), ApiController]
public class RssFeedsController(IRssFeedService service, IRssFeedResultService rssFeedResultService) : CrudController<RssFeed>(service)
{
    [HttpGet("{id}/results"), ProducesResponseType(typeof(IEnumerable<RssFeed>), StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> GetResultsByRssFeedId(string id)
        => HandleRequest(async () => await rssFeedResultService.GetByRssFeedId(id) is { } rssFeedResult
            ? Ok(rssFeedResult)
            : NotFound());

    [HttpGet("status/{stateValue}"), ProducesResponseType(typeof(IEnumerable<RssFeed>), StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> GetFeedsByState(StateValue stateValue)
        => HandleRequest(async () => await rssFeedResultService.GetByStatus(stateValue) is { } rssFeedResult
            ? Ok(rssFeedResult)
            : NotFound());
}