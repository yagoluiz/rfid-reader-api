using Asset.API.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Asset.API.Infrastructure.Configurations
{
    public class AssetItemConfiguration : IEntityTypeConfiguration<AssetItem>
    {
        public void Configure(EntityTypeBuilder<AssetItem> builder)
        {
            builder.ToTable("AssetItem");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.IdentifierNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.SerialNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Epc)
                .IsRequired()
                .HasMaxLength(24);

            builder.Property(x => x.Situation)
                .IsRequired();

            builder.Property(x => x.DateCreated)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.HasOne(x => x.Local)
               .WithMany()
               .HasForeignKey(x => x.AssetLocalId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Type)
               .WithMany()
               .HasForeignKey(x => x.AssetTypeId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
