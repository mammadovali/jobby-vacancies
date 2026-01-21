using Jobby.Domain.Entities.Common;
using Jobby.Domain.Entities.Identity;

namespace Jobby.Domain.Entities.QuestionAggregate
{
    public class QuestionOption : Editable<User>
    {
        public int QuestionId { get; private set; }
        public Question Question { get; private set; }

        public string Text { get; private set; }
        public bool IsCorrect { get; private set; }

        protected QuestionOption() { }

        public QuestionOption(string text, bool isCorrect, int createdById)
        {
            Text = text;
            IsCorrect = isCorrect;
            SetAuditDetails(createdById);
        }
    }

}
