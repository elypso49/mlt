﻿using Microsoft.AspNetCore.Mvc;
using mlt.common.controllers;
using mlt.synology.services;

namespace mlt.api;

[Route("[controller]")]
[ApiController]
public class DownloadStationController(IDownloadStationService service) : BaseController
{
    [HttpGet]
    public Task<ActionResult> GetTasks()
        => HandleRequest(async () =>
                         {
                             var result = await service.GetTasks();

                             return (result != null, Ok(result));
                         });
}