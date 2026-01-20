using Jobby.Application.Constants;
using MediatR;

namespace Jobby.Application.Features.Commands.Category.Delete
{
    public class DeleteCategoryCommand : IRequest<ResponseDto>
    {
        public int Id { get; set; }
        public DeleteCategoryCommand(int id)
        {
            Id = id;
        }
    }
}
