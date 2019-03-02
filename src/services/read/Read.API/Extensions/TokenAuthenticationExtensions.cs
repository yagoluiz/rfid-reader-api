using Microsoft.AspNetCore.Authentication;
using Read.API.Handler;
using System;

namespace Read.API.Extensions
{
    public static class TokenAuthenticationExtensions
    {
        public static AuthenticationBuilder AddBearerToken(this AuthenticationBuilder builder, Action<TokenAuthenticationOptions> configureOptions)
        {
            return builder.AddScheme<TokenAuthenticationOptions, BearerTokenAuthenticationHandler>(TokenAuthenticationOptions.Bearer, configureOptions);
        }
    }
}
