using Jobby.Application.Features.Queries.Category.DTOs;

namespace Jobby.Application.Repositories.Dashboard
{
    public interface IAdminDashboardReadRepository
    {
        Task<List<CategorySuccessRateDto>> GetCategorySuccessRatesAsync();
    }
}
