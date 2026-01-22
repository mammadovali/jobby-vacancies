using Jobby.Application.Exceptions;
using Jobby.Application.Features.Commands.Applicant.DTOs;
using Jobby.Application.Repositories;
using Jobby.Application.Repositories.Applicant;
using Jobby.Application.Repositories.Question;
using Jobby.Domain.Entities.ApplicantAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jobby.Application.Features.Commands.Applicant.FinishTest
{
    public class FinishTestCommandHandler : IRequestHandler<FinishTestCommand, FinishTestResultDto>
    {
        private readonly IApplicantReadRepository _applicantReadRepo;
        private readonly IApplicantAnswerReadRepository _answerReadRepo;
        private readonly IApplicantQuestionProgressReadRepository _progressReadRepo;
        private readonly IApplicantQuestionProgressWriteRepository _progressWriteRepo;
        private readonly ITestResultWriteRepository _testResultWriteRepo;
        private readonly ITestResultReadRepository _testResultReadRepo;
        private readonly IQuestionReadRepository _questionReadRepo;

        public FinishTestCommandHandler(
            IApplicantReadRepository applicantReadRepo,
            IApplicantAnswerReadRepository answerReadRepo,
            IApplicantQuestionProgressReadRepository progressReadRepo,
            IApplicantQuestionProgressWriteRepository progressWriteRepo,
            ITestResultWriteRepository testResultWriteRepo,
            ITestResultReadRepository testResultReadRepo,
            IQuestionReadRepository questionReadRepo)
        {
            _applicantReadRepo = applicantReadRepo;
            _answerReadRepo = answerReadRepo;
            _progressReadRepo = progressReadRepo;
            _progressWriteRepo = progressWriteRepo;
            _testResultWriteRepo = testResultWriteRepo;
            _testResultReadRepo = testResultReadRepo;
            _questionReadRepo = questionReadRepo;
        }

        public async Task<FinishTestResultDto> Handle(
            FinishTestCommand request,
            CancellationToken cancellationToken)
        {
            // 1️. Validate Applicant
            var applicant = await _applicantReadRepo.GetByIdAsync(request.ApplicantId);

            if (applicant == null)
                throw new NotFoundException("Namizəd tapılmadı");

            
            var totalQuestions = await _questionReadRepo
                .GetWhere(q => q.VacancyId == applicant.VacancyId)
                .CountAsync(q => q.VacancyId == applicant.VacancyId);

            // 2️. Get Answers
            var answers = await _answerReadRepo
                .GetWhere(a => a.ApplicantId == request.ApplicantId)
                .ToListAsync(cancellationToken);

            int correctAnswers = answers.Count(a => a.IsCorrect);

            bool textResultExists = await _testResultReadRepo
                .GetWhere(tr => tr.ApplicantId == request.ApplicantId)
                .AnyAsync(cancellationToken);

            if (textResultExists)
                throw new UniqueException("Namizəd üçün artıq test nəticəsi mövcuddur");

            // 3️. Create Test Result
            var testResult = new TestResult(
                applicant.Id,
                totalQuestions,
                correctAnswers);

            await _testResultWriteRepo.AddAsync(testResult);
            await _testResultWriteRepo.SaveAsync();

            // 4. Close active progresses
            var activeProgresses = await _progressReadRepo
                .GetWhere(p =>
                    p.ApplicantId == request.ApplicantId &&
                    !p.IsAnswered)
                .ToListAsync(cancellationToken);

            foreach (var progress in activeProgresses)
            {
                progress.MarkAsAnswered();
                _progressWriteRepo.Update(progress);
            }

            await _progressWriteRepo.SaveAsync();

            // 5️. Result DTO
            return new FinishTestResultDto
            {
                TotalQuestions = testResult.TotalQuestions,
                CorrectAnswers = testResult.CorrectAnswers,
                WrongAnswers = testResult.WrongAnswers,
                ScorePercent = testResult.ScorePercent
            };
        }
    }

}
