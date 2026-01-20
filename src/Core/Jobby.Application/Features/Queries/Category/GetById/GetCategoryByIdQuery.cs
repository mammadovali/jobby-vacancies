using Jobby.Application.Features.Queries.Category.DTOs;
using MediatR;

namespace Jobby.Application.Features.Queries.Category.GetById
{
    public class GetCategoryByIdQuery : IRequest<CategoryDto>
    {
        public int Id { get; set; }

        public GetCategoryByIdQuery(int id)
        {
            Id = id;
        }
    }
}
