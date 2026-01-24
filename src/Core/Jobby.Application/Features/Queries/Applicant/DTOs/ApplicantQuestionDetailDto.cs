namespace Jobby.Application.Features.Queries.Applicant.DTOs
{
    public class ApplicantQuestionDetailDto
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }

        public List<QuestionOptionDetailDto> Options { get; set; }
    }
}
