using Jobby.Application.Features.Commands.Auth.DTOs;
using MediatR;

namespace Jobby.Application.Features.Commands.Auth.Create
{
    public class CreateAuthTokenForAdminCommand : IRequest<JwtTokenDto>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
