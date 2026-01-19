using Jobby.Application.Features.Queries.User.DTOs;
using Jobby.Domain.Entities.Identity;

namespace Jobby.Application.Interfaces.Identity
{
    public interface IUserManager
    {
        int GetCurrentUserId();
        string GetCurrentUserType();
        Task<UserDto> GetUserProfileAsync(int userId);
        Task<(string token, DateTime expireAt)> GenerateJwtTokenForAdmin(User user);
    }
}
