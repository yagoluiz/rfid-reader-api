using Asset.API.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Asset.API.Infrastructure.Configurations
{
    public class AssetItemConfiguration : IEntityTypeConfiguration<AssetItem>
    {
        public void Configure(EntityTypeBuilder<AssetItem> builder)
        {
            throw new NotImplementedException();
        }
    }
}
