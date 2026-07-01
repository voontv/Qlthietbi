using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace QlThietBi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // statusService = new NotiTeleService();
            //statusService.CheckStatusAndSendNotification().GetAwaiter();

            CreateHostBuilder(args).Build().Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseDefaultServiceProvider(_ => { })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
