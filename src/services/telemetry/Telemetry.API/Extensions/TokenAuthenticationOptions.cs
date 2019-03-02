using Microsoft.AspNetCore.Authentication;

namespace Telemetry.API.Extensions
{
    public class TokenAuthenticationOptions : AuthenticationSchemeOptions
    {
        public const string Bearer = "Bearer";
    }
}
