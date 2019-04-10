using HealthChecks.UI.Client;
using Log.API.Extensions;
using Log.API.Features.Log;
using Log.API.Middleware;
using Log.API.Settings;
using Log.API.Swagger;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using NSwag;
using NSwag.SwaggerGeneration.Processors.Security;
using System.IO.Compression;
using System.Reflection;

[assembly: ApiConventionType(typeof(MyApiConventions))]
namespace Log.API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddJsonOptions(x =>
            {
                x.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = TokenAuthenticationOptions.Bearer;
            }).AddBearerToken(null);
            services.AddApiVersioning(x => x.ApiVersionReader = new HeaderApiVersionReader("api-version"));
            services.Configure<GzipCompressionProviderOptions>(x => x.Level = CompressionLevel.Optimal);
            services.AddResponseCompression(x =>
            {
                x.Providers.Add<GzipCompressionProvider>();
            });
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddOpenApiDocument(document =>
            {
                document.DocumentName = "v1";
                document.Version = "v1";
                document.Title = "API Log";
                document.Description = "RFID tag log API";
                document.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT"));
                document.DocumentProcessors.Add(new SecurityDefinitionAppender("JWT", new SwaggerSecurityScheme
                {
                    Type = SwaggerSecuritySchemeType.ApiKey,
                    Name = HeaderNames.Authorization,
                    Description = "Token SSO",
                    In = SwaggerSecurityApiKeyLocation.Header
                }));
            });
            services.AddHealthChecksUI()
                .AddHealthChecks();

            RegisterServices(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IOptions<ApplicationInsightsSettings> options)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseResponseCompression();
            app.UseSwagger();
            app.UseSwaggerUi3();
            app.UseHealthChecks("/health", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            app.UseHealthChecksUI(setup =>
            {
                setup.UIPath = "/health-ui";
            });
            app.UseExceptionHandler(new ExceptionHandlerOptions
            {
                ExceptionHandler = new ErrorHandlerMiddleware(options, env).Invoke
            });
            app.UseMvc();
        }

        #region Services

        private void RegisterServices(IServiceCollection services)
        {
            services.Configure<ApplicationInsightsSettings>(Configuration.GetSection("ApplicationInsights"));

            services.AddSingleton<LogContext>();
            services.AddTransient<ILogRepository, LogRepository>();
        }

        #endregion
    }
}
