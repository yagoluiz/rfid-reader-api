using Asset.API.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Asset.API.Extensions
{
    public static class WebHostExtensions
    {
        public static IWebHost SeedData(this IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetService<AssetDbContext>();

                context.Database.Migrate();

                new AssetDbContextSeed().SeedInitial(context);
            }
            
            return host;
        }
    }
}
