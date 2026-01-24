using Jobby.Domain.Entities.ApplicantAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobby.Persistence.EntityTypeConfiguration.Applicant
{
    public class ApplicantAnswerEntityTypeConfiguration : IEntityTypeConfiguration<ApplicantAnswer>
    {
        public void Configure(EntityTypeBuilder<ApplicantAnswer> builder)
        {
            builder.ToTable("applicant_answers");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Applicant)
                   .WithMany(a => a.ApplicantAnswers)
                   .HasForeignKey(x => x.ApplicantId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Question)
                   .WithMany(a => a.ApplicantAnswers)
                   .HasForeignKey(x => x.QuestionId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.QuestionOption)
                   .WithMany()
                   .HasForeignKey(x => x.QuestionOptionId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.IsCorrect)
                   .IsRequired();

            builder.HasIndex(x => new { x.ApplicantId, x.QuestionId })
                   .IsUnique();
        }
    }

}
