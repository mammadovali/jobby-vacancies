using Jobby.Domain.Entities.Identity;

namespace Jobby.Domain.Entities.Common
{
    public class Editable<TUser> : Auditable<TUser> where TUser : User
    {
        public int? UpdatedById { get; protected set; }
        public DateTime? UpdatedDate { get; protected set; }

        public void SetEditFields(int? updatedById)
        {

            UpdatedById = updatedById;
            UpdatedDate = DateTime.UtcNow;
        }
    }
}
