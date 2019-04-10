using Microsoft.AspNetCore.Authentication;

namespace Log.API.Extensions
{
    public class TokenAuthenticationOptions : AuthenticationSchemeOptions
    {
        public const string Bearer = "Bearer";
    }
}
