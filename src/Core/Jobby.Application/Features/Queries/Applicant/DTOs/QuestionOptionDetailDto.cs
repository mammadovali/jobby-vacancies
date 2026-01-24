namespace Jobby.Application.Features.Queries.Applicant.DTOs
{
    public class QuestionOptionDetailDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public bool IsSelectedByApplicant { get; set; }
    }
}
