using Jobby.Application.Constants;
using Jobby.Application.Features.Queries.Applicant.DTOs;
using Jobby.Application.Repositories;
using Jobby.Application.Repositories.Dashboard;
using Jobby.Domain.Entities.ApplicantAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jobby.Application.Features.Queries.Applicant.GetAll
{
    public class GetApplicantsQueryHandler : IRequestHandler<GetApplicantsQuery, AllDto<ApplicantListDto>>
    {
        private readonly IAdminDashboardReadRepository _dashboardReadRepository;

        public GetApplicantsQueryHandler(IAdminDashboardReadRepository dashboardReadRepository)
        {
            _dashboardReadRepository = dashboardReadRepository;
        }
        public async Task<AllDto<ApplicantListDto>> Handle(GetApplicantsQuery request, CancellationToken cancellationToken)
        {
            var query = _dashboardReadRepository.GetApplicantsQueryable();

            if (request.CategoryId.HasValue)
            {
                query = query.Where(a => a.Vacancy.CategoryId == request.CategoryId.Value);
            }

            if (request.VacancyId.HasValue)
            {
                query = query.Where(a => a.VacancyId == request.VacancyId.Value);
            }

            query = query.ApplySortingAndFiltering(request.ColumnName, request.OrderBy, request.FilterValue);

            int totalCount = await query.CountAsync(cancellationToken);

            int totalPage = request.PageSize != null
                ? (int)Math.Ceiling(totalCount / (double)request.PageSize)
                : 1;

            if (request.PageNumber != null && request.PageSize != null)
            {
                query = query
                    .Skip((request.PageNumber.Value - 1) * request.PageSize.Value)
                    .Take(request.PageSize.Value);
            }

            var data = await query.SelectMany(a => a.TestResults
                .Where(tr => tr.ApplicantId == a.Id), (a, tr) => new ApplicantListDto
                {
                    FullName = a.FirstName + " " + a.LastName,
                    Email = a.Email,
                    VacancyTitle = a.Vacancy.Title,
                    VacancyId = a.Vacancy.Id,
                    CategoryName = a.Vacancy.Category.Name,
                    CategoryId = a.Vacancy.CategoryId,
                    ScorePercent = tr.ScorePercent,
                    AppliedDate = a.CreatedDate
                })
               .ToListAsync(cancellationToken);

            return new AllDto<ApplicantListDto>
            {
                Data = data,
                TotalCount = totalCount,
                TotalPage = (int)Math.Ceiling(
                    totalCount / (decimal)request.PageSize)
            };
        }
    }
}
