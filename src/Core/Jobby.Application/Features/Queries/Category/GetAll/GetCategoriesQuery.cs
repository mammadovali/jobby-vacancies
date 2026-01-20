using Jobby.Application.Constants;
using Jobby.Application.Features.Queries.Category.DTOs;
using MediatR;

namespace Jobby.Application.Features.Queries.Category.GetAll
{
    public class GetCategoriesQuery : IRequest<AllDto<CategoryDto>>
    {
        public string? FilterValue { get; set; }
        public string? ColumnName { get; set; }
        public string? OrderBy { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
