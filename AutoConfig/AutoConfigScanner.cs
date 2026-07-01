using System;
using System.Reflection;
using QlThietBi.AutoConfig;
using Microsoft.Extensions.DependencyInjection;

namespace QlThietBi.AutoConfig
{
    public class AutoConfigScanner
    {
        public static void Scan(IServiceCollection services, Type targetType)
        {
            foreach (var type in targetType.Assembly.GetTypes())
            {
                if (type.GetCustomAttribute<ImplementByAttribute>() is { } implementByAttribute)
                {
                    services.AddScoped(type, implementByAttribute.Type);
                }
            }
            // services.AddScoped(typeof(ILoggerManager<>), typeof(LoggerManager<>));
        }
    }
}