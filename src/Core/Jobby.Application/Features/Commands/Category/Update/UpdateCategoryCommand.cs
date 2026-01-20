using Jobby.Application.Constants;
using MediatR;

namespace Jobby.Application.Features.Commands.Category.Update
{
    public class UpdateCategoryCommand : IRequest<ResponseDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
