using Jobby.Application.Features.Queries.User.DTOs;
using MediatR;

namespace Jobby.Application.Features.Queries.User.Get
{
    public class UserProfileQuery : IRequest<UserDto>
    {
    }
}
