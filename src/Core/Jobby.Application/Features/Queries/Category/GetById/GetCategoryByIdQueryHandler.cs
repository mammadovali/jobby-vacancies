using AutoMapper;
using Jobby.Application.Exceptions;
using Jobby.Application.Features.Queries.Category.DTOs;
using Jobby.Application.Repositories.Category;
using MediatR;

namespace Jobby.Application.Features.Queries.Category.GetById
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryReadRepository _readRepository;

        public GetCategoryByIdQueryHandler(IMapper mapper, ICategoryReadRepository readRepository)
        {
            _mapper = mapper;
            _readRepository = readRepository;
        }
        public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _readRepository.GetByIdAsync(request.Id);

            if (category is null)
                throw new BadRequestException("Kateqoriya tapılmadı");

            var categoryDto = _mapper.Map<CategoryDto>(category);

            return categoryDto;
        }
    }
}
