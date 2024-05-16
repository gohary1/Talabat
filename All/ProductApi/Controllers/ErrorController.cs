using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Error;

namespace ProductApi.Controllers
{
    [Route("Error/{num}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        public ActionResult error(int num)
        {
            if (num == 401) 
            {
                return Unauthorized(new ApiResponse(num));
            }
            else if(num==404)
            {
                return NotFound(new ApiResponse(num));
            }
            else
            {
                return StatusCode(num);
            }

        }
    }
}
