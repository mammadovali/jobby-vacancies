using Jobby.Application.Features.Queries.Applicant.DTOs;
using Jobby.Application.Features.Queries.Category.DTOs;
using Jobby.Application.Repositories.Dashboard;
using Jobby.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Jobby.Persistence.Repositories.Dashboard
{
    public class AdminDashboardReadRepository : IAdminDashboardReadRepository
    {
        private readonly ApplicationDbContext _context;

        public AdminDashboardReadRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategorySuccessRateDto>> GetCategorySuccessRatesAsync()
        {
            return await (
                from tr in _context.TestResults
                join a in _context.Applicants on tr.ApplicantId equals a.Id
                join v in _context.Vacancies on a.VacancyId equals v.Id
                join c in _context.Categories on v.CategoryId equals c.Id
                group tr by new { c.Id, c.Name }
                into g
                select new CategorySuccessRateDto
                {
                    Id = g.Key.Id,
                    Name = g.Key.Name,
                    ApplicantCount = g.Count(),
                    SuccessRate =
                        g.Sum(x => x.TotalQuestions) == 0
                            ? 0
                            : Math.Round(
                                (decimal)g.Sum(x => x.CorrectAnswers)
                                / g.Sum(x => x.TotalQuestions) * 100,
                                2
                            )
                }
            )
            .OrderByDescending(x => x.SuccessRate)
            .ToListAsync();
        }

        public async Task<List<TopApplicantDto>> GetTopApplicantsAsync(int topCount)
        {
            var query =
                from tr in _context.TestResults
                join a in _context.Applicants on tr.ApplicantId equals a.Id
                join v in _context.Vacancies on a.VacancyId equals v.Id
                join c in _context.Categories on v.CategoryId equals c.Id
                where !a.IsDeleted && !v.IsDeleted && !c.IsDeleted
                orderby tr.ScorePercent descending
                select new TopApplicantDto
                {
                    ApplicantId = a.Id,
                    FullName = a.FirstName + " " + a.LastName,
                    Email = a.Email,
                    VacancyTitle = v.Title,
                    VacancyId = v.Id,
                    CategoryName = c.Name,
                    CategoryId = c.Id,
                    ScorePercent = tr.ScorePercent,
                    CompletedAt = tr.CompletedAt
                };

            return await query
                .Take(topCount)
                .AsNoTracking()
                .ToListAsync();
        }

        public IQueryable<Domain.Entities.ApplicantAggregate.Applicant> GetApplicantsQueryable()
        {
            return _context.Applicants
                .AsNoTracking()
                .Include(a => a.TestResults)
                .Include(a => a.Vacancy)
                    .ThenInclude(v => v.Category)
                .Where(a => !a.IsDeleted && a.TestResults.Any());
        }

        public IQueryable<Domain.Entities.ApplicantAggregate.Applicant> GetApplicantDetailQueryable()
        {
            return _context.Applicants
                .AsNoTracking()
                .Include(a => a.Vacancy)
                    .ThenInclude(v => v.Category)
                .Include(a => a.TestResults)
                .Include(a => a.ApplicantAnswers)
                    .ThenInclude(aa => aa.Question)
                        .ThenInclude(q => q.Options)
                .Where(a => !a.IsDeleted);
        }

    }
}
