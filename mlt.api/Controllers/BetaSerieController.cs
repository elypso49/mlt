using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace mlt.api.Controllers;

[ApiController]
[Route("[controller]")]
public class BetaSerieController
{
    [HttpGet("shows")]
    public ActionResult<string> Test()
        => null;
}