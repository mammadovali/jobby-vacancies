using Jobby.Domain.Entities.ApplicantAggregate;
using Jobby.Domain.Entities.CategoryAggregate;
using Jobby.Domain.Entities.Identity;
using Jobby.Domain.Entities.QuestionAggregate;
using Jobby.Domain.Entities.VacancyAggragate;
using Microsoft.EntityFrameworkCore;

namespace Jobby.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionOption> QuestionOptions { get; set; }
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<ApplicantAnswer> ApplicantAnswers { get; set; }
        public DbSet<ApplicantQuestionProgress> ApplicantQuestionProgresses { get; set; }
        public DbSet<TestResult> TestResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
