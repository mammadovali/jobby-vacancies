namespace Jobby.Application.Features.Queries.Applicant.DTOs
{
    public class TopApplicantDto
    {
        public int ApplicantId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        public string VacancyTitle { get; set; }
        public int VacancyId { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }

        public decimal ScorePercent { get; set; }
        public DateTime CompletedAt { get; set; }
    }

}
