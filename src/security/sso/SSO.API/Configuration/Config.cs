using IdentityServer4.Models;
using System.Collections.Generic;

namespace SSO.API.Configuration
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("gateway", "API Gateway")
                {
                    ApiSecrets =
                    {
                        new Secret("O4rupmjm1MKDktEwupUjJ99hj2GSiM".Sha256())
                    },
                    Scopes =
                    {
                        new Scope("gateway.full_access", "Full access to API Gateway")
                    }
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "api_gateway",
                    ClientName = "API Gateway",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedCorsOrigins =
                    {
                        "*"
                    },
                    ClientSecrets =
                    {
                        new Secret("Jhlc4iFC5b2M6Jg2rEihbEXKFJyRbn".Sha256())
                    },
                    AllowedScopes =
                    {
                        "gateway"
                    }
                }
            };
        }
    }
}
