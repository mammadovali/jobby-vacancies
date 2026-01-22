using Jobby.Application.Repositories.Applicant;
using Jobby.Persistence.Context;

namespace Jobby.Persistence.Repositories.Applicant
{
    public class ApplicantWriteRepository : WriteRepository<Domain.Entities.ApplicantAggregate.Applicant>, IApplicantWriteRepository
    {
        public ApplicantWriteRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}