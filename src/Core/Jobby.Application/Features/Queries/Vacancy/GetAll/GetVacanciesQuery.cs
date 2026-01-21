using Jobby.Application.Constants;
using Jobby.Application.Features.Queries.Vacancy.DTOs;
using MediatR;

namespace Jobby.Application.Features.Queries.Vacancy.GetAll
{
    public class GetVacanciesQuery : IRequest<AllDto<VacancyDto>>
    {
        public string? FilterValue { get; set; }
        public string? ColumnName { get; set; }
        public string? OrderBy { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
