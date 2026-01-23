using Jobby.Application.Features.Queries.Question.DTOs;

namespace Jobby.Application.Features.Commands.Applicant.DTOs
{
    public class QuestionApplicantDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Order { get; set; }
        public int TimeLeftSeconds { get; set; }
        public int TotalQuestions { get; set; }

        public List<QuestionOptionApplicantDto> Options { get; set; } = new();
    }
}
