using Jobby.Application.Constants;
using Jobby.Application.Features.Queries.Applicant.DTOs;
using MediatR;

namespace Jobby.Application.Features.Queries.Applicant.GetAll
{
    public class GetApplicantsQuery : IRequest<AllDto<ApplicantListDto>>
    {
        public int? CategoryId { get; set; }
        public int? VacancyId { get; set; }
        public string? FilterValue { get; set; }
        public string? ColumnName { get; set; }
        public string? OrderBy { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
