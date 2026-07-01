using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using log4net;
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace QlThietBi.Handlers
{
    public class ClientIpCheckActionFilter : ActionFilterAttribute
    {
        private static readonly log4net.ILog _logger
           = log4net.LogManager.GetLogger(typeof(ClientIpCheckActionFilter))!;
        private readonly string _safelist;

        public ClientIpCheckActionFilter(string? safelist)
        {
            _safelist = safelist ?? string.Empty;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var realP = context.HttpContext.Request.Headers["X-Real-IP"].ToString();
            if (string.IsNullOrWhiteSpace(realP))
            {
                realP = context.HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
            }

            var ip = _safelist.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            var badIp = true;
            foreach (var address in ip)
            {
                if (address.Equals(realP, StringComparison.OrdinalIgnoreCase))
                {
                    badIp = false;
                    break;
                }
            }

            if (badIp)
            {
                _logger.Info("ClientIpCheckActionFilter ClientIpCheckActionFilter IP không cho phép");
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
                return;
            }
            _logger.Info("ClientIpCheckActionFilter ClientIpCheckActionFilter IP bạn ok được phép truy cập");
            base.OnActionExecuting(context);
        }
    }
}
