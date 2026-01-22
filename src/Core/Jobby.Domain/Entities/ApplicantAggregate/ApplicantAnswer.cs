using Jobby.Domain.Entities.Common;
using Jobby.Domain.Entities.Identity;
using Jobby.Domain.Entities.QuestionAggregate;

namespace Jobby.Domain.Entities.ApplicantAggregate
{
    public class ApplicantAnswer : Editable<User>
    {
        public int ApplicantId { get; private set; }
        public int QuestionId { get; private set; }
        public int? QuestionOptionId { get; private set; }

        public bool IsCorrect { get; private set; }
        public DateTime AnsweredAt { get; private set; }
        public Applicant Applicant { get; private set; }
        public Question Question { get; private set; }
        public QuestionOption QuestionOption { get; private set; }

        protected ApplicantAnswer() { }

        public ApplicantAnswer(
            int applicantId,
            int questionId,
            int? questionOptionId,
            bool isCorrect)
        {
            ApplicantId = applicantId;
            QuestionId = questionId;
            QuestionOptionId = questionOptionId;
            IsCorrect = isCorrect;
            AnsweredAt = DateTime.UtcNow;
        }
    }
}
