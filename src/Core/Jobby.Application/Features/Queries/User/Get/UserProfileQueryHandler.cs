using AutoMapper;
using Jobby.Application.Features.Queries.User.DTOs;
using Jobby.Application.Interfaces.Identity;
using MediatR;

namespace Jobby.Application.Features.Queries.User.Get
{
    public class UserProfileQueryHandler : IRequestHandler<UserProfileQuery, UserDto>
    {
        private readonly IUserManager _userManager;

        public UserProfileQueryHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserDto> Handle(UserProfileQuery request, CancellationToken cancellationToken)
        {
            string userType = _userManager.GetCurrentUserType();

            return await _userManager.GetUserProfileAsync(_userManager.GetCurrentUserId());
            
        }
    }
}
