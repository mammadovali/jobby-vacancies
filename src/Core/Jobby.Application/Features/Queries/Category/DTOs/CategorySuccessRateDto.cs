namespace Jobby.Application.Features.Queries.Category.DTOs
{
    public class CategorySuccessRateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal SuccessRate { get; set; }
        public int ApplicantCount { get; set; }
    }

}
