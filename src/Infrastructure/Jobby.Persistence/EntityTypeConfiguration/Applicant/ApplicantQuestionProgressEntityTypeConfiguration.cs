using Jobby.Domain.Entities.ApplicantAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobby.Persistence.EntityTypeConfiguration.Applicant
{
    public class ApplicantQuestionProgressEntityTypeConfiguration : IEntityTypeConfiguration<ApplicantQuestionProgress>
    {
        public void Configure(EntityTypeBuilder<ApplicantQuestionProgress> builder)
        {
            builder.ToTable("applicant_question_progresses");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Applicant)
                   .WithMany()
                   .HasForeignKey(x => x.ApplicantId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Question)
                   .WithMany()
                   .HasForeignKey(x => x.QuestionId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.QuestionStartedAt)
                   .IsRequired();

            builder.Property(x => x.QuestionExpiresAt)
                   .IsRequired();

            builder.Property(x => x.IsAnswered)
                   .IsRequired();

            builder.HasIndex(x => new { x.ApplicantId, x.QuestionId })
                   .IsUnique();
        }
    }

}
