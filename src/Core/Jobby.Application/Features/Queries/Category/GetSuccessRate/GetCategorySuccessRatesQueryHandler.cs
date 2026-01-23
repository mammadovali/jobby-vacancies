using Jobby.Application.Features.Queries.Category.DTOs;
using Jobby.Application.Repositories.Dashboard;
using MediatR;

namespace Jobby.Application.Features.Queries.Category.GetSuccessRate
{
    public class GetCategorySuccessRatesQueryHandler
    : IRequestHandler<GetCategorySuccessRatesQuery, List<CategorySuccessRateDto>>
    {
        private readonly IAdminDashboardReadRepository _readRepository;

        public GetCategorySuccessRatesQueryHandler(IAdminDashboardReadRepository readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task<List<CategorySuccessRateDto>> Handle(GetCategorySuccessRatesQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _readRepository.GetCategorySuccessRatesAsync();
            return result;
        }
    }
}