using AutoMapper;
using Jobby.Application.Exceptions;
using Jobby.Application.Features.Commands.Applicant.DTOs;
using Jobby.Application.Repositories.Applicant;
using Jobby.Application.Repositories.Question;
using Jobby.Domain.Entities.ApplicantAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jobby.Application.Features.Commands.Applicant.StartTest
{
    public class StartTestCommandHandler : IRequestHandler<StartTestCommand, QuestionApplicantDto>
    {
        private readonly IApplicantReadRepository _applicantReadRepository;
        private readonly IQuestionReadRepository _questionReadRepository;
        private readonly IApplicantQuestionProgressWriteRepository _progressWriteRepository;
        private readonly IMapper _mapper;

        public StartTestCommandHandler(
            IApplicantReadRepository applicantReadRepository,
            IQuestionReadRepository questionReadRepository,
            IApplicantQuestionProgressWriteRepository progressWriteRepository,
            IMapper mapper)
        {
            _applicantReadRepository = applicantReadRepository;
            _questionReadRepository = questionReadRepository;
            _progressWriteRepository = progressWriteRepository;
            _mapper = mapper;
        }

        public async Task<QuestionApplicantDto> Handle(StartTestCommand request, CancellationToken cancellationToken)
        {
            var applicant = await _applicantReadRepository.GetByIdAsync(request.ApplicantId);

            var firstQuestion = await _questionReadRepository
                .GetSingleAsync(q => q.VacancyId == applicant.VacancyId,
                q => q.OrderBy(q => q.Order).Include(q => q.Options));

            if (firstQuestion == null)
                throw new BadRequestException("Bu vakansiya üçün sual tapılmadı");

            var progress = new ApplicantQuestionProgress(applicant.Id, firstQuestion.Id);
            await _progressWriteRepository.AddAsync(progress);
            await _progressWriteRepository.SaveAsync();

            var questionDto = _mapper.Map<QuestionApplicantDto>(firstQuestion);
            questionDto.TimeLeftSeconds = 60;

            return questionDto;
        }
    }
}
