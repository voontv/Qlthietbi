using QlThietBi.Models;
using QlThietBi.Providers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace QlThietBi.Handlers
{
    public class TokenProviderMiddleware
    {
        private readonly RequestDelegate next;

        public TokenProviderMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var authenticateInfo = await context.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);
            var bearerTokenIdentity = authenticateInfo?.Principal;
            if (bearerTokenIdentity?.Identity is ClaimsIdentity identity)
            {
                context.User = new UserClaimsPrincipal(identity);
            }

            await next(context);
        }
    }
}