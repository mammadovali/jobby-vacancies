namespace Jobby.Application.Features.Queries.Applicant.DTOs
{
    public class ApplicantListDto
    {
        public int ApplicantId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string VacancyTitle { get; set; }
        public int VacancyId { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public decimal ScorePercent { get; set; }
        public bool IsFinishedTest { get; set; }

        public DateTime AppliedDate { get; set; }
    }
}
