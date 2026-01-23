using Jobby.Domain.Entities.Common;
using Jobby.Domain.Entities.Identity;
using Jobby.Domain.Entities.VacancyAggragate;

namespace Jobby.Domain.Entities.ApplicantAggregate
{
    public class Applicant : Editable<User>
    {
        public int VacancyId { get; private set; }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string? Phone { get; private set; }
        public string CvFilePath { get; private set; }
        public ICollection<TestResult> TestResults { get; private set; } = new List<TestResult>();

        public Vacancy Vacancy { get; private set; }

        protected Applicant() { }

        public Applicant(
            int vacancyId,
            string firstName,
            string lastName,
            string email,
            string? phone,
            int createdById = 0)
        {
            VacancyId = vacancyId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            SetAuditDetails(createdById);
        }
        public void SetCvPath(string path)
        {
            CvFilePath = path;
        }
    }

}
