using QlThietBi.AutoConfig;
using QlThietBi.Businesses.DonViBoPhan;
using QlThietBi.Businesses.NguoiSuDungThietBi;
using QlThietBi.Businesses.TepDinhKem;
using QlThietBi.Businesses.ThietBi;
using QlThietBi.FileManager;
using QlThietBi.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace QlThietBi.LibsStartup
{
    public static class DIConfig
    {
        public static void RegisterDI(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            AutoConfigScanner.Scan(services, typeof(AutoConfigScanner));
            services.AddSingleton<TokenProviderMiddleware>();
            services.AddScoped<IThietBiBusiness, ThietBiBusiness>();
            services.AddScoped<IDonViBoPhanBusiness, DonViBoPhanBusiness>();
            services.AddScoped<INguoiSuDungThietBiBusiness, NguoiSuDungThietBiBusiness>();
            services.AddScoped<ITepDinhKemBusiness, TepDinhKemBusiness>();
            services.AddTransient<IFileService, FileService>();
        }
    }
}