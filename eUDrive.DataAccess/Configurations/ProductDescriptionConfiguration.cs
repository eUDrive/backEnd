using eUDrive.Domains.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eUDrive.DataAccess.Configurations
{
    public class ProductDescriptionConfiguration : IEntityTypeConfiguration<ProductDescriptionData>
    {
        public void Configure(EntityTypeBuilder<ProductDescriptionData> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Description).HasMaxLength(200);

            builder.HasIndex(d => d.ProductId).IsUnique();

            builder.HasOne(d => d.DescriptionAdvanced)
                .WithOne(a => a.Description)
                .HasForeignKey<DescriptionAdvanced>(a => a.DescriptionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
