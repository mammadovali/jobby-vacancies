using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobby.Persistence.EntityTypeConfiguration.Applicant
{
    public class ApplicantEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Entities.ApplicantAggregate.Applicant>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.ApplicantAggregate.Applicant> builder)
        {
            builder.ToTable("applicants");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Phone).IsRequired().HasMaxLength(50);

            builder.HasOne(x => x.Vacancy)
                .WithMany()
                .HasForeignKey(x => x.VacancyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new { x.VacancyId, x.Email })
               .IsUnique();
        }
    }
}