using Jobby.Application.Repositories.Applicant;
using Jobby.Persistence.Context;

namespace Jobby.Persistence.Repositories.Applicant
{
    public class ApplicantQuestionProgressWriteRepository 
        : WriteRepository<Domain.Entities.ApplicantAggregate.ApplicantQuestionProgress>, IApplicantQuestionProgressWriteRepository
    {
        public ApplicantQuestionProgressWriteRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}