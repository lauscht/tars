using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kipp.Server.Controllers
{
    [Route("auth")]
    public class AuthController : ControllerBase
    {

        [Route("google-login")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleCallback") };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }
    
        [Route("google-callback")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GoogleCallback()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    
            var claims = result.Principal.Identities
                .FirstOrDefault().Claims.Select(claim => new
            {
                claim.Issuer,
                claim.OriginalIssuer,
                claim.Type,
                claim.Value
            });
    
            return new JsonResult(claims);
        }

        [HttpGet]        
        [Route("test")]
        [AllowAnonymous]
        public async Task<ActionResult> Test01()
        {
            return this.Ok();
        }
       
        [HttpPost]      
        [Authorize]  
        [Route("test2")]
        public async Task<ActionResult> Test02()
        {
            return this.Ok();
        }

    }
}