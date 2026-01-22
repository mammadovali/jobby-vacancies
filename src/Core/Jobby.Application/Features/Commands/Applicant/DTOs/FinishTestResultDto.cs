namespace Jobby.Application.Features.Commands.Applicant.DTOs
{
    public class FinishTestResultDto
    {
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public int WrongAnswers { get; set; }
        public decimal ScorePercent { get; set; }
    }
}
