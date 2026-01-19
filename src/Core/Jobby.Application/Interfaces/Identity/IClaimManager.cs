using Jobby.Domain.Entities.Identity;
using System.Security.Claims;

namespace Jobby.Application.Interfaces.Identity
{
    public interface IClaimManager
    {
        int GetCurrentUserId();
        string GetCurrentProfessorId();
        bool CheckUserIsViewer();
        IEnumerable<Claim> GetUserClaims(User user);
        string GetCurrentUserType();
    }
}
