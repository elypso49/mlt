namespace mlt.common.controllers;

public abstract class BaseController : ControllerBase
{
    protected async Task<ActionResult> HandleRequest(Func<Task<(bool Success, object Result)>> process)
    {
        try
        {
            var (success, result) = await process();

            if (success)
                return result is NoContentResult ? NoContent() : (ActionResult)result;

            return NotFound();
        }
        catch (Exception ex)
        {
            
            return BadRequest(new { message = ex.Message });
        }
    }
}