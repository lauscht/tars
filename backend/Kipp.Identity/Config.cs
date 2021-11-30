// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Kipp.Identity
{
    public static class Config
    {
        public const string KippApiScopeName = "app.api.kipp";
        public const string TarsFrontendCliendId = "app.client.tars";
        public const string PostmanCliendId = "app.client.postman";
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };


        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource {
                    Name = "Kipp.Server",
                    DisplayName = "Tars Backend - Kipp",
                    // ApiSecrets = { new Secret("a75a559d-1dab-4c65-9bc0-f8e590cb388d".Sha256()) },
                    Scopes = {
                        KippApiScopeName
                    }
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope(KippApiScopeName),
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = PostmanCliendId,
                    ClientName = "Postman Test - Case",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequirePkce = false,

                    RedirectUris =           { "https://oauth.pstmn.io/v1/browser-callback", "https://oauth.pstmn.io/v1/callback" },
                    PostLogoutRedirectUris = { "https://localhost:5001/index.html" },
                    AllowedCorsOrigins =     { "https://localhost:5001" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        KippApiScopeName
                    }
                },
                new Client
                {
                    ClientId = TarsFrontendCliendId,
                    ClientName = "Tars Frontend - Case",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,

                    RedirectUris =           { "https://localhost:5001/callback.html" },
                    PostLogoutRedirectUris = { "https://localhost:5001/index.html" },
                    AllowedCorsOrigins =     { "https://localhost:5001" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        KippApiScopeName
                    }
                },
            };
    }
}