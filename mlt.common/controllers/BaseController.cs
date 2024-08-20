namespace mlt.common.controllers;

public abstract class BaseController : ControllerBase
{
    protected async Task<IActionResult> HandleRequest(Func<Task<IActionResult>> process)
    {
        try
        {
            return await process();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}