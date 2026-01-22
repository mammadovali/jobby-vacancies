using Jobby.Application.Repositories.Applicant;
using Jobby.Persistence.Context;

namespace Jobby.Persistence.Repositories.Applicant
{
    public class ApplicantQuestionProgressReadRepository
        : ReadRepository<Domain.Entities.ApplicantAggregate.ApplicantQuestionProgress>, IApplicantQuestionProgressReadRepository
    {
        public ApplicantQuestionProgressReadRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}