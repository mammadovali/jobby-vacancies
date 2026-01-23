using Jobby.Application.Features.Queries.Vacancy.DTOs;
using MediatR;

namespace Jobby.Application.Features.Queries.Vacancy.GetTop
{
    public class GetTopVacanciesQuery : IRequest<List<TopVacanciesByCategoryDto>>
    {
        public int TopCount { get; set; }

        public GetTopVacanciesQuery(int topCount)
        {
            TopCount = topCount;
        }
    }
}
