namespace Jobby.Application.Features.Queries.Vacancy.DTOs
{
    public class TopVacanciesByCategoryDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<VacancyShortDto> Vacancies { get; set; } = new();
    }
}
