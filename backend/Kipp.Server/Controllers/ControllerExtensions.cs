using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kipp.Server.Controllers{

    public static class ControllerExtensions{
        
        public static StatusCodeResult ServiceUnavailable(this ControllerBase controller) =>
            new StatusCodeResult(StatusCodes.Status503ServiceUnavailable);
    }
}