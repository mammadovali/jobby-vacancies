using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobby.Persistence.EntityTypeConfiguration.Question
{
    public class QuestionEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Entities.QuestionAggregate.Question>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.QuestionAggregate.Question> builder)
        {
            builder.ToTable("questions");
            builder.HasKey(q => q.Id);

            builder.Property(q => q.Text)
                   .IsRequired()
                   .HasMaxLength(1000);

            builder.Property(q => q.Order)
                   .IsRequired();

            builder.HasOne(q => q.Vacancy)
                   .WithMany(v => v.Questions)
                   .HasForeignKey(q => q.VacancyId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(q => q.Options)
                   .WithOne(o => o.Question)
                   .HasForeignKey(o => o.QuestionId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(q => q.Options)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }

}
