using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobby.Persistence.EntityTypeConfiguration.Category
{
    public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Entities.CategoryAggregate.Category>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.CategoryAggregate.Category> builder)
        {
            builder.ToTable("categories");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasMany(c => c.Vacancies)
                   .WithOne(v => v.Category)
                   .HasForeignKey(v => v.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
