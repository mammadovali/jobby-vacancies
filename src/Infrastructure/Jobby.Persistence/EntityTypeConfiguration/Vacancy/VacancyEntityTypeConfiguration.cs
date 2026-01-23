using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobby.Persistence.EntityTypeConfiguration.Vacancy
{
    public class VacancyEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Entities.VacancyAggragate.Vacancy>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.VacancyAggragate.Vacancy> builder)
        {
            builder.ToTable("vacancies");
            builder.HasKey(v => v.Id);

            builder.Property(v => v.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(v => v.IsActive)
                   .HasDefaultValue(true);

            builder.HasOne(v => v.Category)
                   .WithMany(c => c.Vacancies)
                   .HasForeignKey(v => v.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
