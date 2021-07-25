
using System;
using System.Threading.Tasks;
using Kipp.Framework.Services;
using Kipp.Server.Services;
using Kipp.Server.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Kipp.Server.Controllers.General{

    [ApiController]
    [Route("general/health")]
    public class HealthController: ControllerBase {
        public IDatabaseContext DatabaseContext;

        public HealthController(
            IDatabaseContext databaseContext)
        {
            DatabaseContext = databaseContext ?? throw new ArgumentNullException(nameof(DatabaseContext));
        }

        [HttpGet]
        public async Task<OkResult> Alive() =>
            await Task.FromResult(Ok());

        [HttpGet, Route("database")]
        public async Task<StatusCodeResult> Database()
        {
            // querying the list of collections should be possible.
            var healthy = await DatabaseContext.Healthy();
            return healthy ? Ok() : this.ServiceUnavailable();
        }
    }
}
