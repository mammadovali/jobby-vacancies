using Jobby.Application.Constants;
using MediatR;

namespace Jobby.Application.Features.Commands.Category.Create
{
    public class CreateCategoryCommand : IRequest<ResponseDto>
    {
        public string Name { get; set; }
    }
}
