using QlThietBi.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace QlThietBi.AutoConfig
{
    public class TraceIPAttribute : ActionFilterAttribute
    {
        private static readonly log4net.ILog log
           = log4net.LogManager.GetLogger(typeof(TraceIPAttribute))!;
        IPDetailModel model = new IPDetailModel();
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var remoteIp = context.HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(remoteIp))
            {
                remoteIp = context.HttpContext.Request.Headers["X_Real_IP"].ToString();
            }

            log.Info("Day la ip can tim  " + remoteIp + "  iph check thu " + context.HttpContext.Request.Headers["X_Real_IP"]);
            var sessionValue = context.HttpContext.Session.GetString(remoteIp);
            if (string.IsNullOrWhiteSpace(sessionValue))
            {
                model.Count = 1;
                model.IPAddress = remoteIp;
                model.Time = DateTime.Now;
                context.HttpContext.Session.SetString(remoteIp, JsonConvert.SerializeObject(model));
            }
            else
            {
                var _record = JsonConvert.DeserializeObject<IPDetailModel>(sessionValue);
                if (_record is not null)
                {
                    if (DateTime.Now.Subtract(_record.Time).TotalMinutes < 1 && _record.Count > 1)
                    {
                        context.Result = new JsonResult("Permission denined!");
                        return;
                    }

                    _record.Count = _record.Count + 1;
                    context.HttpContext.Session.Remove(remoteIp);
                    context.HttpContext.Session.SetString(remoteIp, JsonConvert.SerializeObject(_record));
                }
            }
        }
    }
}
