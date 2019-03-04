using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Ocelot.API
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
            void options(IdentityServerAuthenticationOptions identityServer)
            {
                identityServer.Authority = Configuration.GetSection("IdentityServer:Authority").Value;
                identityServer.ApiName = Configuration.GetSection("IdentityServer:ApiName").Value;
                identityServer.ApiSecret = Configuration.GetSection("IdentityServer:ApiSecret").Value;
                identityServer.SupportedTokens = SupportedTokens.Both;
            }

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddCors();
            services.AddOcelot(Configuration);
            services.AddAuthentication().AddIdentityServerAuthentication(Configuration.GetSection("IdentityServer:ProviderKey").Value, options);
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

            app.UseHttpsRedirection();
            app.UseCors(x =>
            {
                x.AllowAnyOrigin();
                x.AllowAnyHeader();
                x.AllowAnyMethod();
                x.AllowCredentials();
            });
            app.UseOcelot().Wait();
        }
    }
}
