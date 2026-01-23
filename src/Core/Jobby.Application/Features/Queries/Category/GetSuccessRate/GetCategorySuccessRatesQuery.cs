using Jobby.Application.Features.Queries.Category.DTOs;
using MediatR;

namespace Jobby.Application.Features.Queries.Category.GetSuccessRate
{
    public class GetCategorySuccessRatesQuery : IRequest<List<CategorySuccessRateDto>>
    {
    }
}