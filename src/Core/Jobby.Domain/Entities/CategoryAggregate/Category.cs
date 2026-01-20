using Jobby.Domain.Entities.Common;
using Jobby.Domain.Entities.Identity;
using Jobby.Domain.Entities.VacancyAggragate;

namespace Jobby.Domain.Entities.CategoryAggregate
{
    public class Category : Editable<User>
    {
        public string Name { get; private set; }

        public ICollection<Vacancy> Vacancies { get; private set; } = new List<Vacancy>();

        protected Category() { }

        public Category(string name, int createdById)
        {
            Name = name;
            SetAuditDetails(createdById);
        }

        public void SetDetails(string name, int updatedById)
        {
            Name = name;
            SetEditFields(updatedById);
        }
    }
}
