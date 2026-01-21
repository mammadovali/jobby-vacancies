namespace Jobby.Application.Features.Queries.Vacancy.DTOs
{
    public class VacancyDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
