// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace Kipp.Identity
{
    public class Config
    {
        public const string KippApiAudienceName = "Kipp.Api";
        public const string KippApiScopeName = "app.api.kipp";
        public const string TarsFrontendCliendId = "app.client.tars";
        public const string PostmanCliendId = "app.client.postman";
        public IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };


        public IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource {
                    Name = KippApiAudienceName,
                    DisplayName = "Tars Backend - Kipp",
                    
                    Scopes = {
                        KippApiScopeName
                    }
                }
            };

        public IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope(KippApiScopeName, new string[] {ClaimTypes.Role, } ),
            };

        public IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = PostmanCliendId,
                    ClientName = "Postman Test - Case",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequirePkce = false,
                    AlwaysIncludeUserClaimsInIdToken = true,

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