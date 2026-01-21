using AutoMapper;
using Jobby.Application.Features.Queries.Question.DTOs;
using Jobby.Application.Repositories.Question;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jobby.Application.Features.Queries.Question.GetByVacancyId
{
    public class GetQuestionsByVacancyIdQueryHandler : IRequestHandler<GetQuestionsByVacancyIdQuery, List<QuestionDto>>
    {
        private readonly IQuestionReadRepository _readRepository;
        private readonly IMapper _mapper;

        public GetQuestionsByVacancyIdQueryHandler(IQuestionReadRepository readRepository,
            IMapper mapper)
        {
            _readRepository = readRepository;
            _mapper = mapper;
        }

        public async Task<List<QuestionDto>> Handle( GetQuestionsByVacancyIdQuery request, CancellationToken cancellationToken)
        {
            var questions = await _readRepository
                .GetWhere(
                    q => q.VacancyId == request.VacancyId && !q.IsDeleted,
                    include => include.Include(q => q.Options)
                )
                .OrderBy(q => q.Order)
                .ToListAsync(cancellationToken);

            if(questions is null || !questions.Any())
                return new List<QuestionDto>();

            return _mapper.Map<List<QuestionDto>>(questions);
        }
    }

}
