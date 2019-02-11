using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NSwag.AspNetCore;
using System.IO.Compression;
using System.Reflection;
using Telemetry.API.Features.Telemetry;
using Telemetry.API.Filters;
using Telemetry.API.Settings;
using Telemetry.API.Swagger;

[assembly: ApiConventionType(typeof(MyApiConventions))]
namespace Telemetry.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(x =>
            {
                x.Filters.Add(typeof(ExceptionServiceFilter));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddJsonOptions(x =>
            {
                x.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });
            services.AddApiVersioning(x => x.ApiVersionReader = new HeaderApiVersionReader("api-version"));
            services.Configure<GzipCompressionProviderOptions>(x => x.Level = CompressionLevel.Optimal);
            services.AddResponseCompression(x =>
            {
                x.Providers.Add<GzipCompressionProvider>();
            });
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddSwaggerDocument(x =>
            {
                x.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Telemetry API";
                };
            });
            services.AddHealthChecks().AddCheck<TelemetryHealthCheck>("TelemetryHealthCheck");

            RegisterServices(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseMvc();
            app.UseHttpsRedirection();
            app.UseResponseCompression();
            app.UseSwagger();
            app.UseSwaggerUi3();
            app.UseHealthChecks("/hc");
        }

        #region Services

        private void RegisterServices(IServiceCollection services)
        {
            services.Configure<ApplicationInsights>(Configuration.GetSection("ApplicationInsights"));

            services.AddScoped<ITelemetryRepository, TelemetryRepository>();
            services.AddScoped<TelemetryContext>();
        }

        #endregion
    }
}
