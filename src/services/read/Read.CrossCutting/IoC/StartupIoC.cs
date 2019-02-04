using Microsoft.Extensions.DependencyInjection;
using Read.Domain.Interfaces.Repository;
using Read.Infra.Context;
using Read.Infra.Repository;

namespace Read.CrossCutting.IoC
{
    public class StartupIoC
    {
        public static void RegisterServices(IServiceCollection services)
        {
            #region Infra

            services.AddSingleton<StorageContext>();
            services.AddScoped<IReadRepository, ReadRepository>();

            #endregion
        }
    }
}
