using Jobby.Application.Exceptions;
using Jobby.Application.Features.Commands.Applicant.DTOs;
using Jobby.Application.Repositories;
using Jobby.Application.Repositories.Applicant;
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

        public FinishTestCommandHandler(
            IApplicantReadRepository applicantReadRepo,
            IApplicantAnswerReadRepository answerReadRepo,
            IApplicantQuestionProgressReadRepository progressReadRepo,
            IApplicantQuestionProgressWriteRepository progressWriteRepo,
            ITestResultWriteRepository testResultWriteRepo)
        {
            _applicantReadRepo = applicantReadRepo;
            _answerReadRepo = answerReadRepo;
            _progressReadRepo = progressReadRepo;
            _progressWriteRepo = progressWriteRepo;
            _testResultWriteRepo = testResultWriteRepo;
        }

        public async Task<FinishTestResultDto> Handle(
            FinishTestCommand request,
            CancellationToken cancellationToken)
        {
            // 1️. Validate Applicant
            var applicant = await _applicantReadRepo.GetByIdAsync(request.ApplicantId);

            if (applicant == null)
                throw new NotFoundException("Namizəd tapılmadı");

            bool isApplied = await _applicantReadRepo
                .GetWhere(a => a.VacancyId == applicant.VacancyId && a.Email == applicant.Email).AnyAsync(cancellationToken);

            if (isApplied)
                throw new BadRequestException("Siz artıq bu vakansiya üçün müraciət etmisiniz");

            // 2️. Get Answers
            var answers = await _answerReadRepo
                .GetWhere(a => a.ApplicantId == request.ApplicantId)
                .ToListAsync(cancellationToken);

            int totalQuestions = answers.Count;
            int correctAnswers = answers.Count(a => a.IsCorrect);
            int wrongAnswers = totalQuestions - correctAnswers;

            // 3️. Create Test Result
            var testResult = new TestResult(
                request.ApplicantId,
                totalQuestions,
                correctAnswers,
                wrongAnswers);

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

            // 5️. Result DTO
            return new FinishTestResultDto
            {
                TotalQuestions = totalQuestions,
                CorrectAnswers = correctAnswers,
                WrongAnswers = wrongAnswers,
                ScorePercent = testResult.ScorePercent
            };
        }
    }

}
