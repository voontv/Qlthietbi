using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using QlThietBi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
            if (authenticateInfo?.Principal?.Identity is ClaimsIdentity identity)
            {
                context.User = new UserClaimsPrincipal(identity);
            }
            else
            {
                var fallbackIdentity = TryReadExternalBearerToken(context);
                if (fallbackIdentity != null)
                {
                    context.User = new UserClaimsPrincipal(fallbackIdentity);
                }
            }

            await next(context);
        }

        private static ClaimsIdentity? TryReadExternalBearerToken(HttpContext context)
        {
            var authorization = context.Request.Headers.Authorization.FirstOrDefault();
            if (string.IsNullOrWhiteSpace(authorization) || !authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            var token = authorization["Bearer ".Length..].Trim();
            if (string.IsNullOrWhiteSpace(token))
            {
                return null;
            }

            try
            {
                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
                if (jwt.ValidTo != DateTime.MinValue && jwt.ValidTo <= DateTime.UtcNow)
                {
                    return null;
                }

                return new ClaimsIdentity(jwt.Claims, JwtBearerDefaults.AuthenticationScheme);
            }
            catch
            {
                return null;
            }
        }
    }
}
