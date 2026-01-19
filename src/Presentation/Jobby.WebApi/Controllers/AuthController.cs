using Jobby.Application.Features.Commands.Auth.Create;
using Jobby.Application.Features.Queries.User.Get;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.WebApi.Controllers
{
    public class AuthController : BaseApiController
    {
        [HttpPost("login")]
        public async Task<IActionResult> AdminLogin(CreateAuthTokenForAdminCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("user-info")]
        public async Task<IActionResult> GetUserInfo([FromQuery] UserProfileQuery query)
        {
            var response = await Mediator.Send(query);
            return Ok(response);
        }
    }
}
