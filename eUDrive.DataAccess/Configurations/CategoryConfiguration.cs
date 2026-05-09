using eUDrive.Domains.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eUDrive.DataAccess.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<CategoryData>
    {
        public void Configure(EntityTypeBuilder<CategoryData> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name).IsRequired().HasMaxLength(20);
        }
    }
}
