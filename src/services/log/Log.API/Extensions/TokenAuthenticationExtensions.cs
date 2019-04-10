using Log.API.Handler;
using Microsoft.AspNetCore.Authentication;
using System;

namespace Log.API.Extensions
{
    public static class TokenAuthenticationExtensions
    {
        public static AuthenticationBuilder AddBearerToken(this AuthenticationBuilder builder, Action<TokenAuthenticationOptions> configureOptions)
        {
            return builder.AddScheme<TokenAuthenticationOptions, BearerTokenAuthenticationHandler>(TokenAuthenticationOptions.Bearer, configureOptions);
        }
    }
}
