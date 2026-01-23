using Jobby.Application.Features.Queries.Applicant.DTOs;
using Jobby.Application.Features.Queries.Category.DTOs;

namespace Jobby.Application.Repositories.Dashboard
{
    public interface IAdminDashboardReadRepository
    {
        Task<List<CategorySuccessRateDto>> GetCategorySuccessRatesAsync();
        Task<List<TopApplicantDto>> GetTopApplicantsAsync(int topCount);
    }
}
