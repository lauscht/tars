// CRUD for Enduser Lessons

using System;
using System.Threading.Tasks;
using Kipp.Framework.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kipp.Server.Controllers.Enduser{

    [ApiController]
    [Authorize]
    [Route("enduser/authentication")]
    public class AuthenticationController: ControllerBase {

        [HttpGet]
        public OkObjectResult Get()
        {
            var identity = this.HttpContext.User.Identity.Name;
            return Ok(identity);
        }
    }
}