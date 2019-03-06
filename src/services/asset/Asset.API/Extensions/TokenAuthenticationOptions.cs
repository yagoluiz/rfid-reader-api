using Microsoft.AspNetCore.Authentication;

namespace Asset.API.Extensions
{
    public class TokenAuthenticationOptions : AuthenticationSchemeOptions
    {
        public const string Bearer = "Bearer";
    }
}
