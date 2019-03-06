using Asset.API.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Asset.API.Infrastructure.Configurations
{
    public class AssetLocalConfiguration : IEntityTypeConfiguration<AssetLocal>
    {
        public void Configure(EntityTypeBuilder<AssetLocal> builder)
        {
            builder.ToTable("AssetLocal");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Description)
                .IsRequired(false)
                .HasMaxLength(250);
        }
    }
}
