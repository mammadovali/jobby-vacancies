using Jobby.Application.Exceptions;
using Jobby.Application.Features.Queries.Applicant.DTOs;
using Jobby.Application.Repositories.Dashboard;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jobby.Application.Features.Queries.Applicant.GetById
{
    public class GetApplicantDetailQueryHandler
    : IRequestHandler<GetApplicantByIdQuery, ApplicantDetailDto>
    {
        private readonly IAdminDashboardReadRepository _repository;

        public GetApplicantDetailQueryHandler(
            IAdminDashboardReadRepository repository)
        {
            _repository = repository;
        }

        public async Task<ApplicantDetailDto> Handle(
            GetApplicantByIdQuery request,
            CancellationToken cancellationToken)
        {
            var applicant = await _repository
            .GetApplicantDetailQueryable()
            .FirstOrDefaultAsync(a => a.Id == request.Id,cancellationToken);

            if (applicant is null)
                throw new NotFoundException("Namizəd tapılmadı");

            var testResult = applicant.TestResults.FirstOrDefault();
            if (testResult is null)
                throw new NotFoundException("Namizədin test nəticəsi tapılmadı");

            var questions = applicant.ApplicantAnswers
                .GroupBy(a => a.Question)
                .OrderBy(g => g.Key.Order)
                .Select(g => new ApplicantQuestionDetailDto
                {
                    QuestionId = g.Key.Id,
                    QuestionText = g.Key.Text,
                    Options = g.Key.Options.Select(opt => new QuestionOptionDetailDto
                    {
                        Id = opt.Id,
                        Text = opt.Text,
                        IsCorrect = opt.IsCorrect,
                        IsSelectedByApplicant =
                            g.Any(a => a.QuestionOptionId == opt.Id)
                    }).ToList()
                })
                .ToList();

            return new ApplicantDetailDto
            {
                FullName = applicant.FirstName + " " + applicant.LastName,
                Email = applicant.Email,
                Phone = applicant.Phone,
                VacancyId = applicant.Vacancy.Id,
                VacancyTitle = applicant.Vacancy.Title,
                CategoryName = applicant.Vacancy.Category.Name,
                TotalQuestions = testResult.TotalQuestions,
                CorrectAnswers = testResult.CorrectAnswers,
                WrongAnswers = testResult.WrongAnswers,
                ScorePercent = testResult.ScorePercent,
                Questions = questions
            };
        }
    }

}
