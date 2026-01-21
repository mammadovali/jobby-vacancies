using Jobby.Application.Constants;
using MediatR;

namespace Jobby.Application.Features.Commands.Question.Delete
{
    public class DeleteQuestionCommand : IRequest<ResponseDto>
    {
        public int Id { get; set; }

        public DeleteQuestionCommand(int id)
        {
            Id = id;
        }
    }
}