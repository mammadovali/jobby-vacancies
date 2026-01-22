using Jobby.Application.Repositories.Question;
using Jobby.Persistence.Context;

namespace Jobby.Persistence.Repositories.Question
{
    public class QuestionOptionWriteRepository : WriteRepository<Domain.Entities.QuestionAggregate.QuestionOption>, IQuestionOptionWriteRepository
    {
        public QuestionOptionWriteRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
