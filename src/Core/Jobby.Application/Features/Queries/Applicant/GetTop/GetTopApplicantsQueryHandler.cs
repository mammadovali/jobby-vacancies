using Jobby.Application.Features.Queries.Applicant.DTOs;
using Jobby.Application.Repositories.Dashboard;
using MediatR;

namespace Jobby.Application.Features.Queries.Applicant.GetTop
{
    public class GetTopApplicantsQueryHandler : IRequestHandler<GetTopApplicantsQuery, List<TopApplicantDto>>
    {
        private readonly IAdminDashboardReadRepository _dashboardReadRepository;

        public GetTopApplicantsQueryHandler(
            IAdminDashboardReadRepository dashboardReadRepository)
        {
            _dashboardReadRepository = dashboardReadRepository;
        }

        public async Task<List<TopApplicantDto>> Handle( GetTopApplicantsQuery request,
            CancellationToken cancellationToken)
        {
            var topCount = request.TopCount <= 0 ? 5 : request.TopCount;

            return await _dashboardReadRepository.GetTopApplicantsAsync(topCount);
        }
    }
}
