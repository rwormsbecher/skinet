using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace API;

[Microsoft.AspNetCore.Mvc.Route("errors/{code}")]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController : BaseApiController
{

    public IActionResult Error(int code)
    {
        return new ObjectResult(new ApiResponse(code));
    }
}
