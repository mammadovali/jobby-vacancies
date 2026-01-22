using Jobby.Application.Repositories.Applicant;
using Jobby.Persistence.Context;

namespace Jobby.Persistence.Repositories.Applicant
{
    public class TestResultReadRepository : ReadRepository<Domain.Entities.ApplicantAggregate.TestResult>, ITestResultReadRepository
    {
        public TestResultReadRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
