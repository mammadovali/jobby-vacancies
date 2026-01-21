using Jobby.Application.Repositories.Question;
using Jobby.Persistence.Context;

namespace Jobby.Persistence.Repositories.Question
{
    public class QuestionWriteRepository : WriteRepository<Domain.Entities.QuestionAggregate.Question>, IQuestionWriteRepository
    {
        public QuestionWriteRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}