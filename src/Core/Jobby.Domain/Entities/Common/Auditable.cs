using Jobby.Domain.Entities.Identity;

namespace Jobby.Domain.Entities.Common
{
    public class Auditable<TUser> : Entity where TUser : User
    {
        public int CreatedById { get; protected set; }

        public void SetAuditDetails(int createdById)
        {
            CreatedById = createdById;
        }
    }
}
