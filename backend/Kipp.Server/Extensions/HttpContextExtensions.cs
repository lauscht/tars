using Kipp.Framework.Models.Identities;
using Microsoft.AspNetCore.Http;

namespace Kipp.Server.Extensions {
    public static class HttpContextExtension {
        public static UserIdentity GetUserIdentity(this HttpContext httpContext)
        {
            return new UserIdentity(httpContext.User.Identity.Name);
        }
    }
}