using AutoMapper;
using Jobby.Application.Exceptions;
using Jobby.Application.Features.Queries.Question.DTOs;
using Jobby.Application.Repositories.Question;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jobby.Application.Features.Queries.Question.GetById
{
    public class GetQuestionByIdQueryHandler : IRequestHandler<GetQuestionByIdQuery, QuestionDto>
    {
        private readonly IQuestionReadRepository _readRepository;
        private readonly IMapper _mapper;

        public GetQuestionByIdQueryHandler(IQuestionReadRepository readRepository,
            IMapper mapper)
        {
            _readRepository = readRepository;
            _mapper = mapper;
        }

        public async Task<QuestionDto> Handle(GetQuestionByIdQuery request, CancellationToken cancellationToken)
        {
            var question = await _readRepository
                .GetByIdAsync(request.Id,
                    include => include.Include(q => q.Options)
                );

            if (question is null)
                throw new BadRequestException("Sual tapılmadı");

            return _mapper.Map<QuestionDto>(question);
        }
    }
}
