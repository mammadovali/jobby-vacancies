namespace Jobby.Application.Features.Queries.Applicant.DTOs
{
    public class ApplicantDetailDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        //Cv file path or url
        public int VacancyId { get; set; }
        public string VacancyTitle { get; set; }
        public string CategoryName { get; set; }
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public int WrongAnswers { get; set; }
        public decimal ScorePercent { get; set; }
        public List<ApplicantQuestionDetailDto> Questions { get; set; }
    }
}
