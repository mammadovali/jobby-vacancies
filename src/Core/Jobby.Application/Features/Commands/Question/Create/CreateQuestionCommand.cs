using Jobby.Application.Constants;
using Jobby.Application.Features.Commands.Question.DTOs;
using MediatR;

namespace Jobby.Application.Features.Commands.Question.Create
{
    public class CreateQuestionCommand : IRequest<ResponseDto>
    {
        public int VacancyId { get; set; }
        public string Text { get; set; }
        public int Order { get; set; }

        public List<QuestionOptionDto> Options { get; set; } = new();
    }
}
