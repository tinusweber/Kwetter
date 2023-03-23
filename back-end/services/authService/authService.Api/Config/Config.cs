using IdentityServer4.Models;

namespace authService.Api.Config
{
    public class Config
    {
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
             {
                 new ApiScope(name: "read",   displayName: "Read your data."),
                 new ApiScope(name: "write",  displayName: "Write your data."),
                 new ApiScope(name: "delete", displayName: "Delete your data."),
                 new ApiScope(name: "apiscope", displayName: "Api Scope.")
             };
        }
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("myresourceapi", "My Resource API")
                {
                    Scopes = {"apiscope","scope2"},
                    UserClaims = new List<string> {"foo"}
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                // for public api
                new Client
                {
                    ClientId = "secret_client_id",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "apiscope" },
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AlwaysSendClientClaims = true,

                },

                 new Client
                {
                    ClientId = "secret_user_client_id",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "apiscope" },
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AlwaysSendClientClaims = true,

                }
            };
        }
    }
}
