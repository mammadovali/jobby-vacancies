using Jobby.Domain.Entities.Common;
using Jobby.Infrastructure.Operations;

namespace Jobby.Domain.Entities.Identity
{
    public class User : Entity
    {
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public string? RefreshToken { get; private set; }

        public User(string email, string passwordHash)
        {
            Email = email;
            PasswordHash = passwordHash;
            IsDeleted = false;
        }

        public void SetDetails(string fullname, string email, string password)
        {
            FullName = fullname;
            Email = email;
            IsDeleted = false;
            PasswordHash = PasswordHasher.HashPassword(password);
        }

        public void SetProfile(string fullname)
        {
            FullName = fullname;
        }
        public void UpdateRefreshToken(string refreshToken)
        {
            RefreshToken = refreshToken;
        }
    }
}
