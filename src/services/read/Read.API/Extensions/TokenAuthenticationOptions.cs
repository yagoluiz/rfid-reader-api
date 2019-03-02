using Microsoft.AspNetCore.Authentication;

namespace Read.API.Extensions
{
    public class TokenAuthenticationOptions : AuthenticationSchemeOptions
    {
        public const string Bearer = "Bearer";
    }
}
