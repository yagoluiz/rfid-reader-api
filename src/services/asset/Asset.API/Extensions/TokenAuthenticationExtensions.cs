using Asset.API.Handler;
using Microsoft.AspNetCore.Authentication;
using System;

namespace Asset.API.Extensions
{
    public static class TokenAuthenticationExtensions
    {
        public static AuthenticationBuilder AddBearerToken(this AuthenticationBuilder builder, Action<TokenAuthenticationOptions> configureOptions)
        {
            return builder.AddScheme<TokenAuthenticationOptions, BearerTokenAuthenticationHandler>(TokenAuthenticationOptions.Bearer, configureOptions);
        }
    }
}
