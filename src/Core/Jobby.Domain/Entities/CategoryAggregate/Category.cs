using Jobby.Domain.Entities.Common;
using Jobby.Domain.Entities.Identity;

namespace Jobby.Domain.Entities.CategoryAggregate
{
    public class Category : Editable<User>
    {
        public string Name { get; private set; }
        public bool IsActive { get; private set; }

        public void SetDetails(string name, bool isActive)
        {
            Name = name;
            IsActive = isActive;
        }
    }
}
