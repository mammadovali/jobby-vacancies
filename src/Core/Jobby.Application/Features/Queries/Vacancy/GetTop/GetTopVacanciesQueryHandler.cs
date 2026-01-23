using Jobby.Application.Features.Queries.Vacancy.DTOs;
using Jobby.Application.Repositories.Category;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jobby.Application.Features.Queries.Vacancy.GetTop
{
    public class GetTopVacanciesQueryHandler : IRequestHandler<GetTopVacanciesQuery, List<TopVacanciesByCategoryDto>>
    {
        private readonly ICategoryReadRepository _categoryRead;

        public GetTopVacanciesQueryHandler(ICategoryReadRepository categoryRead)
        {
            _categoryRead = categoryRead;
        }

        public async Task<List<TopVacanciesByCategoryDto>> Handle(
            GetTopVacanciesQuery request,
            CancellationToken cancellationToken)
        {

            var topCount = request.TopCount <= 0 ? 5 : request.TopCount;
            var query = await _categoryRead
                .GetWhereSimple(c => c.Vacancies.Any(v => v.IsActive && !v.IsDeleted))
                .Select(c => new TopVacanciesByCategoryDto
                {
                    CategoryId = c.Id,
                    CategoryName = c.Name,
                    Vacancies = c.Vacancies
                        .Where(v => v.IsActive && !v.IsDeleted)
                        .OrderByDescending(v => v.Applicants.Count)
                        .Take(request.TopCount)
                        .Select(v => new VacancyShortDto
                        {
                            Id = v.Id,
                            Title = v.Title,
                            ApplicantCount = v.Applicants.Count
                        })
                        .ToList()
                })
                .OrderBy(c => c.CategoryName)
                .ToListAsync(cancellationToken);

            return query;
        }
    }

}
