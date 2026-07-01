using System.Net;

namespace QlThietBi.Handlers
{
    public class AdminSafeListMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string[] _safelist;
        private static readonly log4net.ILog log
           = log4net.LogManager.GetLogger(typeof(AdminSafeListMiddleware));
        public AdminSafeListMiddleware(
            RequestDelegate next,
            string? safelist)
        {
            var ips = (safelist ?? string.Empty).Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            log.Info("AdminSafeListMiddleware  AdminSafeListMiddleware Day IP trong config la ips     " + safelist);
            _safelist = ips;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {        
            await _next.Invoke(context);
        }
    }
}
