using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SSO.API.Certs;
using SSO.API.Configuration;

namespace SSO.API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddCors();
            services.AddIdentityServer()
                .AddSigningCredential(Certificate.Get())
                .AddInMemoryIdentityResources(Config.GetResources())
                .AddInMemoryApiResources(Config.GetApis())
                .AddInMemoryClients(Config.GetClients());
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            ForwardedHeadersOptions fordwardedHeaderOptions = new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            };
            fordwardedHeaderOptions.KnownNetworks.Clear();
            fordwardedHeaderOptions.KnownProxies.Clear();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseForwardedHeaders(fordwardedHeaderOptions).UseCors(x =>
            {
                x.AllowAnyOrigin();
                x.AllowAnyHeader();
                x.AllowAnyMethod();
                x.AllowCredentials();
            });
            app.UseIdentityServer();
        }
    }
}
