using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.WebApi.Controllers
{
    public class TestController : BaseApiController
    {
        [HttpGet]
        public IActionResult Test()
        {
            return Ok("API is working");
        }
    }
}
