using Jobby.Application.Repositories.Applicant;
using Jobby.Persistence.Context;

namespace Jobby.Persistence.Repositories.Applicant
{
    public class TestResultWriteRepository : WriteRepository<Domain.Entities.ApplicantAggregate.TestResult>, ITestResultWriteRepository
    {
        public TestResultWriteRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}