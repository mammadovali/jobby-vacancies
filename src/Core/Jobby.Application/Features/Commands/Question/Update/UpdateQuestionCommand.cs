using Jobby.Application.Constants;
using Jobby.Application.Features.Commands.Question.DTOs;
using MediatR;

namespace Jobby.Application.Features.Commands.Question.Update
{
    public class UpdateQuestionCommand : IRequest<ResponseDto>
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Order { get; set; }

        public List<QuestionOptionDto> Options { get; set; } = new();
    }
}
