using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Kipp.Identity
{
    ///<remarks>
    /// Code base from IdentityServer4 Sample Template so:
    /// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
    /// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
    ///</remarks>
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IIdentityServerInteractionService Interaction;
        private readonly IWebHostEnvironment Environment;
        private readonly ILogger Logger;

        public HomeController(
            IIdentityServerInteractionService interaction, 
            IWebHostEnvironment environment, 
            ILogger<HomeController> logger)
        {
            this.Interaction = interaction;
            this.Environment = environment;
            this.Logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Shows the error page
        /// </summary>
        public async Task<IActionResult> Error(string errorId)
        {
            // retrieve error details from identityserver
            var message = await Interaction.GetErrorContextAsync(errorId);
            if (message != null)
            {
                if (!Environment.IsDevelopment())
                {
                    // only show in development
                    message.ErrorDescription = null;
                    return this.NoContent();
                }
            }

            return this.Content(message.ErrorDescription);
        }
    }
}