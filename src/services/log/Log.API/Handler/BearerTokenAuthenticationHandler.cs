using Log.API.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Log.API.Handler
{
    public class BearerTokenAuthenticationHandler : AuthenticationHandler<TokenAuthenticationOptions>
    {
        private readonly IConfiguration _configuration;
        private const string BearerScheme = "Bearer";
        private string _exceptionMessage = null;

        public BearerTokenAuthenticationHandler(IOptionsMonitor<TokenAuthenticationOptions> options,
            ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IConfiguration configuration)
            : base(options, logger, encoder, clock)
        {
            _configuration = configuration;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue(HeaderNames.Authorization, out var authorization))
            {
                return await Task.FromResult(AuthenticateResult.NoResult());
            }

            var bearerToken = authorization.First().Substring(BearerScheme.Length).Trim();

            if (string.IsNullOrEmpty(bearerToken))
            {
                return await Task.FromResult(AuthenticateResult.NoResult());
            }

            var jwtToken = new JwtSecurityToken(bearerToken);

            if (jwtToken is null)
            {
                return await Task.FromResult(AuthenticateResult.NoResult());
            }

            var validationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new X509SecurityKey(new X509Certificate2(Convert.FromBase64String("MIIC9TCCAd2gAwIBAgIQfWvuNVlhtLhO1E83X+wMvjANBgkqhkiG9w0BAQsFADASMRAwDgYDVQQDEwdpZC5yZmlkMB4XDTE5MDMwMjE1NDI1MVoXDTM5MTIzMTIzNTk1OVowEjEQMA4GA1UEAxMHaWQucmZpZDCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAMLQ5fpLe///W5+pdHVXaItj5p0rr/jh25mtxZFN0L0BXd49bjo4MsajRltR5P7lgBRRikxxuY5DUvmywdHWmGkGT8r4qGtYFkPU4BfVa9Awe3jN9b2j3uw+bC55YgvYu5OLP+djiWwhtjE/1ZMvCCd4rQ+wYMZ3q5xKWZT/diAR+FBFOW8oEiW+eFEe8m+6i03EOnrQvLdAFKEg34ll1LO+rkHdkRxvEmWfIlcIlcMS+RVcvXGgI9bCrRwKGifxl3eeB8QXVtP/tCjzotRxFI81lSpzR88mw+aLQVNXmw/dnzBGWFTyLXYzylg8ePfdfFwhNEmxkeD46vG1/q7yqTkCAwEAAaNHMEUwQwYDVR0BBDwwOoAQsXw6AmuOnqJ2YSgZTjYOHaEUMBIxEDAOBgNVBAMTB2lkLnJmaWSCEH1r7jVZYbS4TtRPN1/sDL4wDQYJKoZIhvcNAQELBQADggEBABHctsGJ6sGVnOJ1Z1NY1KanEf2sK5xt+i2024SER/VmNVt2WnPEfcSBRSssNpCIeFO7u0Rhh6TZNITIuqGrnz7d4S//XYxw1mWMhJwZBf+WUEhx6w4N6oROGkrN6HTNQP6zY+fHtPq9ysYjm5FCe/vzNdd4JM/zqGjJj26TMpLKYV7uAbwwjaB0Y0qQPF7A/DZ0SiVlgZYGyZA3NgNBmUkAJPGJ82phh2VlliuqXc6GtoDnAhLIBX8iK6eutqxoYUFJzaiIVppXgS5uogt9c2ysZimrtOy79lcs481b4AboeOaq1UezNN6QfH1Pu/tUFmmQzdBNV2FitxcZVafzink="))),
                ValidAudience = $"{_configuration["TokenValidationParameters:ValidAudience"]}/resources",
                ValidIssuer = _configuration["TokenValidationParameters:ValidAudience"]
            };

            ClaimsPrincipal jwtClaimPrincipal = null;

            try
            {
                jwtClaimPrincipal = new JwtSecurityTokenHandler().ValidateToken(bearerToken, validationParameters, out var validatedToken);
            }
            catch (Exception ex)
            {
                _exceptionMessage = ex.Message;
                return await Task.FromResult(AuthenticateResult.NoResult());
            }

            var jwtClaimScope = jwtClaimPrincipal.Claims.FirstOrDefault(x => x.Type == "scope")?.Value;

            if (string.IsNullOrEmpty(jwtClaimScope))
            {
                return await Task.FromResult(AuthenticateResult.NoResult());
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.System, jwtClaimScope)
            };

            var claimsPrincipal = new ClaimsPrincipal(jwtClaimPrincipal);
            claimsPrincipal.AddIdentity(new ClaimsIdentity(claims));

            return await Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(new ClaimsPrincipal(claimsPrincipal), BearerScheme)));
        }

        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            Response.Headers.Add(HeaderNames.WWWAuthenticate, BearerScheme);

            if (!string.IsNullOrEmpty(_exceptionMessage))
            {
                var exceptionMessage = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(_exceptionMessage));

                Response.Body.Write(exceptionMessage, 0, exceptionMessage.Length);
            }

            return Task.CompletedTask;
        }
    }
}