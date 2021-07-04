using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kipp.Server.Controllers
{
    [Route("auth")]
    public class AuthController : ControllerBase
    {

        [HttpGet]        
        [AllowAnonymous]
        [Route("test")]
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