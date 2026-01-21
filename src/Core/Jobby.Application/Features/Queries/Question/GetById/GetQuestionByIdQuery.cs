using Jobby.Application.Features.Queries.Question.DTOs;
using MediatR;

namespace Jobby.Application.Features.Queries.Question.GetById
{
    public class GetQuestionByIdQuery : IRequest<QuestionDto>
    {
        public int Id { get; set; }

        public GetQuestionByIdQuery(int id)
        {
            Id = id;
        }
    }
}