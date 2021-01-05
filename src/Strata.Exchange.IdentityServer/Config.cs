// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Strata.Exchange
{
    public static class AadJwtClaimTypes
    {
        public const string ObjectId = "oid";

        public const string PreferredUsername = "preferred_username";

        public const string TenantId = "tid";

        public const string Roles = "roles";
    }
    public static class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            var claimTypes = new[]
            {
                JwtClaimTypes.Subject, JwtClaimTypes.Profile, JwtClaimTypes.IdentityProvider,
                AadJwtClaimTypes.ObjectId, AadJwtClaimTypes.TenantId,
                "tenant_id", JwtClaimTypes.Role, JwtClaimTypes.Name, JwtClaimTypes.PreferredUserName,
                "display_name", "site_group_id",
                JwtClaimTypes.Email,
                JwtClaimTypes.GivenName,
                JwtClaimTypes.MiddleName,
                JwtClaimTypes.FamilyName,
                JwtClaimTypes.Locale,
                "timezone"
            };

            return new List<ApiResource>
            {
                new ApiResource(
                "api", 
                "Strata API",
                claimTypes)
                {
                    Scopes = {"strata.api"}
                }
            };
        }

        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            { 
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            { 
                new ApiScope("strata.api","Strata.Exchange")
            };

        public static IEnumerable<Client> Clients =>
            new Client[] 
            { 
                new Client()
                {
                    ClientId = "strata",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "strata.api" }
                },
                 new Client
                    {
                        ClientId = "strata-api",
                        ClientSecrets = { new Secret("secret".Sha256()) },

                        AllowedGrantTypes = GrantTypes.Code,

                        AllowAccessTokensViaBrowser = true,
                        RequireConsent = false,
                        RedirectUris = { "https://localhost:8184/" },
                        PostLogoutRedirectUris = { "https://localhost:8184" },
                        AllowedCorsOrigins = { "https://localhost:8184"},
                        EnableLocalLogin = true,
                        AllowedScopes = new List<string>
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                            "strata.api"
                        }
                    }
            };
    }
}