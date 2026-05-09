using eUDrive.Domains.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eUDrive.DataAccess.Configurations
{
    public class ProductImgDataConfiguration : IEntityTypeConfiguration<ProductImgData>
    {
        public void Configure(EntityTypeBuilder<ProductImgData> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Url).IsRequired();

            builder.HasIndex(i => i.ProductId);
        }
    }
}
