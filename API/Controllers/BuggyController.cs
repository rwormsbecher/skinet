using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API;

public class BuggyController : BaseApiController
{
    private readonly StoreContext context;

    public BuggyController(StoreContext context)
    {
        this.context = context;
    }


    [HttpGet("testauth")]
    [Authorize]
    public ActionResult<string> GetSecretText()
    {
        return "secret stuff";
    }

    [HttpGet("notfound")]
    public IActionResult GetNotFoundRequest()
    {
        var result = context.Products.Find(53);
        if (result == null)
        {
            return NotFound(new ApiResponse(404));
        }

        return Ok();
    }

    [HttpGet("servererror")]
    public IActionResult GetServerError()
    {
        var result = context.Products.Find(53);
        var thing = result.ToString();


        return Ok();
    }

    [HttpGet("badrequest")]
    public IActionResult GetBadRequest()
    {
        return BadRequest(new ApiResponse(400));
    }

    [HttpGet("badrequest/{id}")]
    public IActionResult GetServerError2(int id)
    {
        return Ok();
    }

}
