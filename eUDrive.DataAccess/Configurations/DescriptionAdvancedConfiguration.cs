using eUDrive.Domains.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eUDrive.DataAccess.Configurations
{
    public class DescriptionAdvancedConfiguration : IEntityTypeConfiguration<DescriptionAdvanced>
    {
        public void Configure(EntityTypeBuilder<DescriptionAdvanced> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasIndex(a => a.DescriptionId).IsUnique();
        }
    }
}