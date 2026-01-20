using AutoMapper;
using Jobby.Application.Features.Queries.Category.DTOs;

namespace Jobby.Application.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Domain.Entities.CategoryAggregate.Category, CategoryDto>();
        }
    }
}
