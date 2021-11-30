using IdentityModel;
using IdentityServer4;
using IdentityServer4.Events;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Kipp.Identity.Services;
using Kipp.Identity.Models;
using Kipp.Identity.Models.Identities;

namespace Kipp.Identity
{    
    ///<remarks>
    /// Code base from IdentityServer4 Sample Template so:
    /// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
    /// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
    ///</remarks>
    [AllowAnonymous]
    public class ExternalController : Controller
    {
        private readonly IUserRepository Users;
        private readonly IIdentityServerInteractionService InteractionService;
        private readonly IClientStore ClientStore;
        private readonly ILogger<ExternalController> Logger;
        private readonly IEventService EventService;

        public ExternalController(
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IEventService events,
            ILogger<ExternalController> logger,
            IUserRepository users)
        {
            // if the TestUserStore is not in DI, then we'll just use the global users collection
            // this is where you would plug in your own custom identity management library (e.g. ASP.NET Identity)
            this.Users = users ?? throw new ArgumentNullException(nameof(users));

            this.InteractionService = interaction;
            this.ClientStore = clientStore;
            this.Logger = logger;
            this.EventService = events;
        }

        /// <summary>
        /// initiate roundtrip to external authentication provider
        /// </summary>
        [HttpGet]
        public IActionResult Challenge(string scheme, string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl)) returnUrl = "~/";

            // validate returnUrl - either it is a valid OIDC URL or back to a local page
            if (Url.IsLocalUrl(returnUrl) == false && InteractionService.IsValidReturnUrl(returnUrl) == false)
            {
                // user might have clicked on a malicious link - should be logged
                throw new Exception("invalid return URL");
            }
            
            // start challenge and roundtrip the return URL and scheme 
            var props = new AuthenticationProperties
            {
                RedirectUri = Url.Action(nameof(Callback)), 
                Items =
                {
                    { "returnUrl", returnUrl }, 
                    { "scheme", scheme },
                }
            };

            return Challenge(props, scheme);
            
        }

        /// <summary>
        /// Post processing of external authentication
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Callback()
        {
            // read external identity from the temporary cookie
            var result = await HttpContext.AuthenticateAsync(IdentityServerConstants.ExternalCookieAuthenticationScheme);
            if (result?.Succeeded != true)
            {
                throw new Exception("External authentication error");
            }

            if (Logger.IsEnabled(LogLevel.Debug))
            {
                var externalClaims = result.Principal.Claims.Select(c => $"{c.Type}: {c.Value}");
                Logger.LogDebug("External claims: {@claims}", externalClaims);
            }

            // lookup our user and external provider info
            var (user, providerIdentity) = await IdentifyUserFromExternalProvider(result);

            // this allows us to collect any additional claims or properties
            // for the specific protocols used and store them in the local auth cookie.
            // this is typically used to store data needed for signout from those protocols.
            var additionalLocalClaims = new List<Claim>();
            var localSignInProps = new AuthenticationProperties();
            ProcessLoginCallback(result, additionalLocalClaims, localSignInProps);
            
            additionalLocalClaims.Add(new Claim(ClaimTypes.Role, "developer"));
            
            // issue authentication cookie for user
            var isuser = new IdentityServerUser(user.Identity)
            {
                DisplayName = user.Username,
                IdentityProvider = providerIdentity.Provider,
                AdditionalClaims = additionalLocalClaims
            };

            await HttpContext.SignInAsync(isuser, localSignInProps);

            // delete temporary cookie used during external authentication
            await HttpContext.SignOutAsync(IdentityServerConstants.ExternalCookieAuthenticationScheme);

            // retrieve return URL
            var returnUrl = result.Properties.Items["returnUrl"] ?? "~/";

            // check if external login is in the context of an OIDC request
            var context = await InteractionService.GetAuthorizationContextAsync(returnUrl);
            await EventService.RaiseAsync(new UserLoginSuccessEvent(
                providerIdentity.Provider, providerIdentity.Identity, user.Identity, user.Username, true, context?.Client.ClientId));
            
            return Redirect(returnUrl);
        }

        private async Task<(User user, ProviderIdentity providerIdentity)> IdentifyUserFromExternalProvider(AuthenticateResult result)
        {

            var (userIdClaim, claims) = ExtractUserIdAndClaimsFromClaimsPrincipal(result.Principal);

            // Create an identity for external user.
            var providerIdentity = new ProviderIdentity()
            {
                Provider = result.Properties.Items["scheme"],
                Identity = userIdClaim.Value
            };

            // find external user
            var user = await Users.GetByProviderIdentity(providerIdentity);
            
            if (user is null)
            {
                // this might be where you might initiate a custom workflow for user registration
                // in this sample we don't show how that would be done, as our sample implementation
                // simply auto-provisions new external user
                user = await AutoProvisionUser(result.Principal, providerIdentity, claims);
            }

            return (user, providerIdentity);
        }

        private (Claim, List<Claim>) ExtractUserIdAndClaimsFromClaimsPrincipal(ClaimsPrincipal principal) 
        {
            // try to determine the unique id of the external user (issued by the provider)
            // the most common claim type for that are the sub claim and the NameIdentifier
            // depending on the external provider, some other claim type might be used
            var userIdClaim = principal.FindFirst(JwtClaimTypes.Subject) ??
                              principal.FindFirst(ClaimTypes.NameIdentifier) ??
                              throw new Exception("Unknown userid");

            // remove the user id claim so we don't include it as an extra claim if/when we provision the user
            var claims = principal.Claims.ToList();
            claims.Remove(userIdClaim);

            return (userIdClaim, claims);
        }

        private string ExtractUserNameFromClaimsPrincipal(ClaimsPrincipal principal, List<Claim> claims) 
        {
            var claim = principal.FindFirst(JwtClaimTypes.Name) ??
                        principal.FindFirst(ClaimTypes.Name);
            if (claim is null)
                return String.Empty;
            
            claims.Remove(claim);
            return claim.Value;
        } 

        private string ExtractUserEMailFromClaimsPrincipal(ClaimsPrincipal principal, List<Claim> claims) 
        {
            var claim = principal.FindFirst(JwtClaimTypes.Email) ??
                        principal.FindFirst(ClaimTypes.Email);
            if (claim is null)
                return String.Empty;
            
            claims.Remove(claim);
            return claim.Value;
        } 

        private async Task<User> AutoProvisionUser(ClaimsPrincipal principal, ProviderIdentity providerIdentity, List<Claim> claims)
        {
            var userName = ExtractUserNameFromClaimsPrincipal(principal, claims);
            var userMail = ExtractUserEMailFromClaimsPrincipal(principal, claims);

            var user = Kipp.Identity.Models.User.Create(providerIdentity);
            user.Username = userName;
            user.Email = userMail;

            await Users.Create(user);
            return user;
        }

        // if the external login is OIDC-based, there are certain things we need to preserve to make logout work
        // this will be different for WS-Fed, SAML2p or other protocols
        private void ProcessLoginCallback(AuthenticateResult externalResult, List<Claim> localClaims, AuthenticationProperties localSignInProps)
        {
            // if the external system sent a session id claim, copy it over
            // so we can use it for single sign-out
            var sid = externalResult.Principal.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.SessionId);
            if (sid != null)
            {
                localClaims.Add(new Claim(JwtClaimTypes.SessionId, sid.Value));
            }

            // if the external provider issued an id_token, we'll keep it for signout
            var idToken = externalResult.Properties.GetTokenValue("id_token");
            if (idToken != null)
            {
                localSignInProps.StoreTokens(new[] { new AuthenticationToken { Name = "id_token", Value = idToken } });
            }
        }
    }
}