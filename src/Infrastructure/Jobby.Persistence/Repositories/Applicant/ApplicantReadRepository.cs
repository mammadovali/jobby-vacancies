using Jobby.Application.Repositories.Applicant;
using Jobby.Persistence.Context;

namespace Jobby.Persistence.Repositories.Applicant
{
    public class ApplicantReadRepository : ReadRepository<Domain.Entities.ApplicantAggregate.Applicant>, IApplicantReadRepository
    {
        public ApplicantReadRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
