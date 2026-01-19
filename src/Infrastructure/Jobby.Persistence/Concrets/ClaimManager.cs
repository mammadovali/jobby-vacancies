using Jobby.Application.Interfaces.Identity;
using Jobby.Application.Repositories.User;
using Jobby.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;

namespace Jobby.Persistence.Concrets
{
    public class ClaimManager : IClaimManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserReadRepository _readRepository;

        public ClaimManager(IHttpContextAccessor httpContextAccessor, IUserReadRepository readRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _readRepository = readRepository;
        }

        public int GetCurrentUserId()
        {
            var claim = GetUserClaims(ClaimTypes.NameIdentifier);

            if (!int.TryParse(claim.Value, out var currentUserId))
                throw new AuthenticationException("Can't Parse this claim Value.");
            return currentUserId;
        }

        public string GetCurrentProfessorId()
        {
            var claim = GetUserClaims(ClaimTypes.SerialNumber);

            return claim.Value;
        }

        public string GetCurrentUserType()
        {
            var userType = _httpContextAccessor.HttpContext.User.FindFirst("userType")?.Value;
            return userType;
        }

        public bool CheckUserIsViewer()
        {
            var isViewer = _httpContextAccessor.HttpContext.User.FindFirst("isViewer")?.Value;

            if (isViewer.Equals("true"))
                return true;
            else
                return false;
        }

        public Claim GetUserClaims(string claimType)
        {
            var user = _httpContextAccessor.HttpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                throw new AuthenticationException("User is not Authenticated.");
            }
            var claim = user.FindFirst(claimType);

            if (claim == null)
            {
                throw new AuthenticationException("User does not have required claim.");
            }
            return claim;
        }
        public IEnumerable<Claim> GetUserClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
            };

            return claims;
        }
    }
}
