using AutoMapper;
using Jobby.Application.Constants;
using Jobby.Application.Features.Queries.Category.DTOs;
using Jobby.Application.Repositories.Category;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jobby.Application.Features.Queries.Category.GetAll
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, AllDto<CategoryDto>>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryReadRepository _readRepository;

        public GetCategoriesQueryHandler(IMapper mapper,
            ICategoryReadRepository readRepository)
        {
            _mapper = mapper;
            _readRepository = readRepository;
        }
        public async Task<AllDto<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = _readRepository.GetWhere(l => !l.IsDeleted);

            categories = categories.ApplySortingAndFiltering(request.ColumnName, request.OrderBy, request.FilterValue);

            int totalCount = await categories.CountAsync(cancellationToken);

            int totalPage = request.PageSize != null
                ? (int)Math.Ceiling(totalCount / (double)request.PageSize)
                : 1;

            if (request.PageNumber != null && request.PageSize != null)
            {
                categories = categories
                    .Skip((request.PageNumber.Value - 1) * request.PageSize.Value)
                    .Take(request.PageSize.Value);
            }

            List<CategoryDto> categoryDtos = _mapper.Map<List<CategoryDto>>(await categories.ToListAsync(cancellationToken));

            return new AllDto<CategoryDto>
            {
                Data = categoryDtos,
                TotalCount = totalCount,
                TotalPage = totalPage,
            };
        }
    }
}
