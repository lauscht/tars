
using System;
using System.Threading.Tasks;
using Kipp.Framework.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kipp.Server.Controllers.General{

    [ApiController]
    [Route("general/health")]
    public class HealthController: ControllerBase {

        [HttpGet]
        public async Task<OkResult> Alive() =>
            await Task.FromResult(Ok());
    }
}
