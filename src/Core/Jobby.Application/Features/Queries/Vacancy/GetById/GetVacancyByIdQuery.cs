using Jobby.Application.Features.Queries.Vacancy.DTOs;
using MediatR;

namespace Jobby.Application.Features.Queries.Vacancy.GetById
{
    public class GetVacancyByIdQuery : IRequest<VacancyDto>
    {
        public int Id { get; set; }

        public GetVacancyByIdQuery(int id)
        {
            Id = id;
        }
    }
}
