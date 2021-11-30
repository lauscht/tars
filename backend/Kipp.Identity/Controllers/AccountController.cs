using Kipp.Identity.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Kipp.Identity
{

    ///<remarks>
    /// Code base from IdentityServer4 Sample Template so:
    /// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
    /// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
    ///</remarks>
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IAuthenticationSchemeProvider SchemeProvider;        

        public AccountController(IAuthenticationSchemeProvider schemeProvider)
        {            
            this.SchemeProvider = schemeProvider;            
        }

        /// <summary>
        /// Entry point into the login workflow
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            var schemes = await this.SchemeProvider.GetAllSchemesAsync();
            var providers = schemes
                           .Where(x => x.DisplayName != null)
                           .ToList();

            return RedirectToAction(
                nameof(ExternalController.Challenge),
                "External",
                new { scheme = providers.First().Name, returnUrl }
            );
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {   
            var user = new User() {
                Username = HttpContext.User.Identity.Name
            };
            return View(user);
        }
    }
}
