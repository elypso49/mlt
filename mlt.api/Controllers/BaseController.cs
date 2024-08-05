using Microsoft.AspNetCore.Mvc;

namespace mlt.api.Controllers;

public class BaseController : ControllerBase
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
            // Log the exception if needed
            // _logger.LogError(ex, "An error occurred while processing the request.");
            return BadRequest(new { message = ex.Message });
        }
    }

    // protected async Task<ActionResult> HandleRequest(Func<Task<ActionResult>> process)
    // {
    //     try
    //     {
    //         return await process();
    //     }
    //     catch (Exception ex)
    //     {
    //         return BadRequest(new { message = ex.Message });
    //     }
    // }
}