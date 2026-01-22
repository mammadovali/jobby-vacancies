using Jobby.Application.Repositories.Question;
using Jobby.Persistence.Context;

namespace Jobby.Persistence.Repositories.QuestionOption
{
    public class QuestionOptionReadRepository : ReadRepository<Domain.Entities.QuestionAggregate.QuestionOption>, IQuestionOptionReadRepository
    {
        public QuestionOptionReadRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
