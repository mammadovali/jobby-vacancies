using AutoMapper;
using Jobby.Application.Exceptions;
using Jobby.Application.Features.Commands.Applicant.DTOs;
using Jobby.Application.Repositories.Applicant;
using Jobby.Application.Repositories.Question;
using Jobby.Domain.Entities.ApplicantAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jobby.Application.Features.Commands.Applicant.SubmitAnswer
{
    public class SubmitAnswerCommandHandler : IRequestHandler<SubmitAnswerCommand, SubmitAnswerResultDto>
    {
        private readonly IApplicantAnswerWriteRepository _answerWriteRepo;
        private readonly IApplicantAnswerReadRepository _answerReadRepo;
        private readonly IApplicantQuestionProgressReadRepository _progressReadRepo;
        private readonly IApplicantQuestionProgressWriteRepository _progressWriteRepo;
        private readonly IQuestionReadRepository _questionReadRepo;
        private readonly IQuestionOptionReadRepository _optionReadRepo;
        private readonly IMapper _mapper;

        public SubmitAnswerCommandHandler(
            IApplicantAnswerWriteRepository answerWriteRepo,
            IApplicantAnswerReadRepository answerReadRepo,
            IApplicantQuestionProgressReadRepository progressReadRepo,
            IApplicantQuestionProgressWriteRepository progressWriteRepo,
            IQuestionReadRepository questionReadRepo,
            IQuestionOptionReadRepository optionReadRepo,
            IMapper mapper)
        {
            _answerWriteRepo = answerWriteRepo;
            _answerReadRepo = answerReadRepo;
            _progressReadRepo = progressReadRepo;
            _progressWriteRepo = progressWriteRepo;
            _questionReadRepo = questionReadRepo;
            _optionReadRepo = optionReadRepo;
            _mapper = mapper;
        }

        public async Task<SubmitAnswerResultDto> Handle(SubmitAnswerCommand request,  CancellationToken cancellationToken)
        {
            // 1️. First we find the active progress
            var progress = await _progressReadRepo.GetSingleAsync(p =>
                p.ApplicantId == request.ApplicantId &&
                p.QuestionId == request.QuestionId &&
                !p.IsAnswered);

            if (progress == null)
                throw new NotFoundException("Aktiv sual tapılmadı");

            // 2️. If the progress is expired, we do not check the answer
            bool isExpired = progress.IsExpired();

            if (isExpired)
                throw new BusinessException("Sual üçün təyin edilən vaxt artıq bitib");

            bool isCorrect = false;

            if (!isExpired && request.QuestionOptionId.HasValue)
            {
                var option = await _optionReadRepo.GetSingleAsync(o =>
                    o.Id == request.QuestionOptionId.Value &&
                    o.QuestionId == request.QuestionId);

                if (option == null)
                    throw new BusinessException("Invalid question option");

                isCorrect = option.IsCorrect;
            }


            // 3️. Write the answer
            var answer = new ApplicantAnswer(
                request.ApplicantId,
                request.QuestionId,
                request.QuestionOptionId,
                isCorrect);

            await _answerWriteRepo.AddAsync(answer);
            await _answerWriteRepo.SaveAsync();

            // 4️. Change progress to answered
            progress.MarkAsAnswered();
            _progressWriteRepo.Update(progress);
            await _progressWriteRepo.SaveAsync();

            // 5️. If there is a next question, create progress for it
            var nextQuestion = await GetNextQuestionAsync(request.QuestionId, request.ApplicantId);

            var nextQuestionDto = _mapper.Map<QuestionApplicantDto>(nextQuestion);

            if (nextQuestion != null)
            {
                var nextProgress = new ApplicantQuestionProgress(
                    request.ApplicantId,
                    nextQuestion.Id);

                await _progressWriteRepo.AddAsync(nextProgress);
                await _progressWriteRepo.SaveAsync();

                return new SubmitAnswerResultDto
                {
                    IsFinished = false,
                    NextQuestion = nextQuestionDto
                };
            }

            // 6️. Test is finished
            return new SubmitAnswerResultDto
            {
                IsFinished = true
            };
        }

        #nullable enable
        private async Task<Domain.Entities.QuestionAggregate.Question?> GetNextQuestionAsync(
            int currentQuestionId,
            int applicantId)
        {
            var currentQuestion = await _questionReadRepo
                .GetSingleAsync(q => q.Id == currentQuestionId, i => i.Include(q => q.Vacancy));

            if (currentQuestion == null)
                return null;

            var answeredQuestionIds = _answerReadRepo
                .GetWhere(a => a.ApplicantId == applicantId)
                .Select(a => a.QuestionId);

            return await _questionReadRepo
                .GetSingleAsync(q =>
                    q.VacancyId == currentQuestion.VacancyId &&
                    q.Order > currentQuestion.Order &&
                    !answeredQuestionIds.Contains(q.Id),
                    include => include.OrderBy(q => q.Order).Include(q => q.Options));
        }
    }

}
