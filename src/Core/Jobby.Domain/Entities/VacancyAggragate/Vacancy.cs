using Jobby.Domain.Entities.CategoryAggregate;
using Jobby.Domain.Entities.Common;
using Jobby.Domain.Entities.Identity;

namespace Jobby.Domain.Entities.VacancyAggragate
{
    public class Vacancy : Editable<User>
    {
        public int CategoryId { get; private set; }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public bool IsActive { get; private set; } = true!;

        public Category Category { get; private set; }
        //public ICollection<Question> Questions { get; private set; } = new List<Question>();

        protected Vacancy() { } // EF Core üçün

        public Vacancy(int categoryId, string title, string description, int createdById)
        {
            CategoryId = categoryId;
            Title = title;
            Description = description;
            IsActive = true;

            SetAuditDetails(createdById);
        }

        public void Update(string title, string description, int updatedById)
        {
            Title = title;
            Description = description;
            SetEditFields(updatedById);
        }

        public void Activate(int updatedById)
        {
            IsActive = true;
            SetEditFields(updatedById);
        }

        public void Deactivate(int updatedById)
        {
            IsActive = false;
            SetEditFields(updatedById);
        }
    }
}
