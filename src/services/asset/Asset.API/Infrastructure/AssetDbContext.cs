using Asset.API.Infrastructure.Configurations;
using Asset.API.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Asset.API.Infrastructure
{
    public class AssetDbContext : DbContext
    { 
        public AssetDbContext(DbContextOptions<AssetDbContext> options)
            : base(options) { }

        public DbSet<AssetItem> AssetItems { get; set; }
        public DbSet<AssetLocal> AssetLocals { get; set; }
        public DbSet<AssetType> AssetTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AssetItemConfiguration());
            modelBuilder.ApplyConfiguration(new AssetLocalConfiguration());
            modelBuilder.ApplyConfiguration(new AssetTypeConfiguration());
        }
    }
}
