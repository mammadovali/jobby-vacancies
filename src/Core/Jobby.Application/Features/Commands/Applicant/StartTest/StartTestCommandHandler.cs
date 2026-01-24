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
        private readonly IApplicantQuestionProgressReadRepository _progressReadRepository;

        private readonly IMapper _mapper;

        public StartTestCommandHandler(
            IApplicantReadRepository applicantReadRepository,
            IQuestionReadRepository questionReadRepository,
            IApplicantQuestionProgressWriteRepository progressWriteRepository,
            IApplicantQuestionProgressReadRepository progressReadRepository,
            IMapper mapper)
        {
            _applicantReadRepository = applicantReadRepository;
            _questionReadRepository = questionReadRepository;
            _progressWriteRepository = progressWriteRepository;
            _progressReadRepository = progressReadRepository;
            _mapper = mapper;
        }

        public async Task<QuestionApplicantDto> Handle(StartTestCommand request, CancellationToken cancellationToken)
        {
            var applicant = await _applicantReadRepository.GetByIdAsync(request.ApplicantId,
                i => i.Include(a => a.Vacancy).ThenInclude(v => v.Questions));

            if (applicant == null)
                throw new NotFoundException("Namizəd tapılmadı");

            var firstQuestion = await _questionReadRepository
                .GetSingleAsync(q => q.VacancyId == applicant.VacancyId,
                q => q.OrderBy(q => q.Order).Include(q => q.Options));

            if (firstQuestion == null)
                throw new BadRequestException("Bu vakansiya üçün sual tapılmadı");

            
            var progress = await _progressReadRepository.GetSingleAsync(
                p => p.ApplicantId == applicant.Id &&
                     p.QuestionId == firstQuestion.Id);

            if (progress == null)
            {
                progress = new ApplicantQuestionProgress(applicant.Id, firstQuestion.Id);
                await _progressWriteRepository.AddAsync(progress);
                await _progressWriteRepository.SaveAsync();
            }

            var questionDto = _mapper.Map<QuestionApplicantDto>(firstQuestion);

            /*questionDto.TimeLeftSeconds = (int) (progress.QuestionExpiresAt - DateTime.UtcNow).TotalSeconds;

            if (questionDto.TimeLeftSeconds < 0)
                questionDto.TimeLeftSeconds = 0;*/
            questionDto.TimeLeftSeconds = 60;
            questionDto.TotalQuestions = applicant.Vacancy.Questions?.Where(q => !q.IsDeleted).Count() ?? 0;

            return questionDto;
        }
    }
}
