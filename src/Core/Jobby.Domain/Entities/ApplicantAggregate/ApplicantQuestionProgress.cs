using Jobby.Domain.Entities.Common;
using Jobby.Domain.Entities.Identity;
using Jobby.Domain.Entities.QuestionAggregate;

namespace Jobby.Domain.Entities.ApplicantAggregate
{
    public class ApplicantQuestionProgress : Editable<User>
    {
        public int ApplicantId { get; private set; }
        public int QuestionId { get; private set; }
        public DateTime QuestionStartedAt { get; private set; }
        public DateTime QuestionExpiresAt { get; private set; }
        public bool IsAnswered { get; private set; }
        public Applicant Applicant { get; private set; }
        public Question Question { get; private set; }

        protected ApplicantQuestionProgress() { }

        public ApplicantQuestionProgress(int applicantId, int questionId)
        {
            ApplicantId = applicantId;
            QuestionId = questionId;
            QuestionStartedAt = DateTime.UtcNow;
            QuestionExpiresAt = QuestionStartedAt.AddSeconds(60);
            IsAnswered = false;
        }

        public void MarkAsAnswered()
        {
            IsAnswered = true;
        }

        public bool IsExpired()
            => DateTime.UtcNow > QuestionExpiresAt;
    }
}
