using Microsoft.Extensions.DependencyInjection;
using Monitoring.Domain.Interfaces.Repository;
using Monitoring.Infra.Context;
using Monitoring.Infra.Repository;

namespace Monitoring.CrossCutting.IoC
{
    public class StartupIoC
    {
        public static void RegisterServices(IServiceCollection services)
        {
            #region Infra

            services.AddSingleton<DeviceContext>();
            services.AddScoped<ITelemetryRepository, TelemetryRepository>();

            #endregion
        }
    }
}
