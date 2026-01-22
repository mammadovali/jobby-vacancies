using Jobby.Application.Repositories.Applicant;
using Jobby.Persistence.Context;

namespace Jobby.Persistence.Repositories.Applicant
{
    public class ApplicantAnswerWriteRepository : WriteRepository<Domain.Entities.ApplicantAggregate.ApplicantAnswer>, IApplicantAnswerWriteRepository
    {
        public ApplicantAnswerWriteRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}