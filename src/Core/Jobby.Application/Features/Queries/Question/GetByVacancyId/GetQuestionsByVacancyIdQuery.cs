using Jobby.Application.Constants;
using Jobby.Application.Features.Queries.Question.DTOs;
using MediatR;

namespace Jobby.Application.Features.Queries.Question.GetByVacancyId
{
    public class GetQuestionsByVacancyIdQuery : IRequest<List<QuestionDto>>
    {
        public int VacancyId { get; set; }

        public GetQuestionsByVacancyIdQuery(int vacancyId)
        {
            VacancyId = vacancyId;
        }
    }
}
