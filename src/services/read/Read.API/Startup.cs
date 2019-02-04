using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using Polly;
using Read.API.Filters;
using Read.API.Services;
using Read.API.Services.Interfaces;
using Read.CrossCutting.IoC;
using Read.CrossCutting.Settings;
using Read.Infra.Context;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.IO.Compression;

namespace Read.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(x =>
            {
                x.Filters.Add(typeof(ExceptionServiceFilter));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });
            services.AddHttpClient<StorageContext>()
                .AddTransientHttpErrorPolicy(policyBuilder =>
                policyBuilder.CircuitBreakerAsync(handledEventsAllowedBeforeBreaking: 2, durationOfBreak: TimeSpan.FromMinutes(1)
            ));
            services.AddApiVersioning(x => x.ApiVersionReader = new HeaderApiVersionReader("api-version"));
            services.Configure<GzipCompressionProviderOptions>(x => x.Level = CompressionLevel.Optimal);
            services.AddResponseCompression(x =>
            {
                x.Providers.Add<GzipCompressionProvider>();
            });
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Read API",
                    Description = "Readings performed by the RFID device"
                });

                if (PlatformServices.Default.Application.ApplicationName != "testhost")
                {
                    x.IncludeXmlComments(Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, $"{PlatformServices.Default.Application.ApplicationName}.xml"));
                }
            });
            services.AddAutoMapper();

            RegisterServices(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (!env.IsProduction())
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
            app.UseSwagger(x =>
            {
                x.RouteTemplate = "swagger/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "Read API - V1");
            });
        }

        #region Services

        private void RegisterServices(IServiceCollection services)
        {
            services.Configure<ApplicationInsights>(Configuration.GetSection("ApplicationInsights"));

            services.AddScoped<IReadService, ReadService>();

            StartupIoC.RegisterServices(services);
        }

        #endregion
    }
}
