using Asset.API.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Asset.API.Infrastructure.Configurations
{
    public class AssetLocalConfiguration : IEntityTypeConfiguration<AssetLocal>
    {
        public void Configure(EntityTypeBuilder<AssetLocal> builder)
        {
            throw new NotImplementedException();
        }
    }
}
