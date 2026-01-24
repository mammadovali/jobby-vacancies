using Jobby.Domain.Entities.ApplicantAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobby.Persistence.EntityTypeConfiguration.Applicant
{
    public class TestResultEntityTypeConfiguration
    : IEntityTypeConfiguration<TestResult>
    {
        public void Configure(EntityTypeBuilder<TestResult> builder)
        {
            builder.ToTable("test_results");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.TotalQuestions).IsRequired();
            builder.Property(x => x.CorrectAnswers).IsRequired();
            builder.Property(x => x.WrongAnswers).IsRequired();
            builder.Property(x => x.CompletedAt).IsRequired();

            builder.HasOne(x => x.Applicant)
                .WithMany(a => a.TestResults)
                .HasForeignKey(x => x.ApplicantId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.ScorePercent)
                   .HasPrecision(5, 2);

            builder.HasIndex(x => x.ApplicantId)
                   .IsUnique();
        }
    }

}
