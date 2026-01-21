using Jobby.Application.Repositories.Question;
using Jobby.Persistence.Context;

namespace Jobby.Persistence.Repositories.Question
{
    public class QuestionReadRepository : ReadRepository<Domain.Entities.QuestionAggregate.Question>, IQuestionReadRepository
    {
        public QuestionReadRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
