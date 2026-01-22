using Jobby.Application.Repositories.Applicant;
using Jobby.Persistence.Context;

namespace Jobby.Persistence.Repositories.Applicant
{
    public class ApplicantAnswerReadRepository : ReadRepository<Domain.Entities.ApplicantAggregate.ApplicantAnswer>, IApplicantAnswerReadRepository
    {
        public ApplicantAnswerReadRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}