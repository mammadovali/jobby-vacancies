using Jobby.Domain.Entities.Common;
using Jobby.Domain.Entities.Identity;

namespace Jobby.Domain.Entities.ApplicantAggregate
{
    public class TestResult : Editable<User>
    {
        public int ApplicantId { get; private set; }

        public int TotalQuestions { get; private set; }
        public int CorrectAnswers { get; private set; }
        public int WrongAnswers { get; private set; }
        public decimal ScorePercent { get; private set; }

        public DateTime CompletedAt { get; private set; }

        public Applicant Applicant { get; private set; }

        protected TestResult() { }

        public TestResult(int applicantId, int totalQuestions, int correctAnswers, int wrongAnswers)
        {
            ApplicantId = applicantId;
            TotalQuestions = totalQuestions;
            CorrectAnswers = correctAnswers;
            WrongAnswers = wrongAnswers;

            ScorePercent = totalQuestions == 0
                ? 0
                : Math.Round((decimal)correctAnswers / totalQuestions * 100, 2);

            CompletedAt = DateTime.UtcNow;
        }
    }
}
