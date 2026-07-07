using QlThietBi.LibsStartup;
using QlThietBi.Providers;
using Microsoft.AspNetCore.Rewrite;
using System.Text;
using System.Text.Json.Serialization;
using BusinessLogic.Providers;
using JsonOptions = Microsoft.AspNetCore.Mvc.JsonOptions;
using Microsoft.EntityFrameworkCore;
using QlThietBi.Models;
using QlThietBi.AutoConfig;
using QlThietBi.Handlers;

namespace QlThietBi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //var connection = Configuration.GetConnectionString("ITDawacoApiDatabase");
            //services.AddDbContext<ITDawacoApiContext>(options => options.UseSqlServer(connection));
            Console.OutputEncoding = Encoding.UTF8;
            services.AddControllers();
            //services.ConfigSecurity(Providers<SecuritySettings>(services));
            services.ConfigSecurity(Config<SecuritySettings>(services));
            services.Configure<WebConfig>(Configuration.GetSection("WebConfig"));
            services.AddDbContext<QlThietBiContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ITDawacoApiDatabase")));
            services.RegisterDI();
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddMvc(FilterHelper.Register).AddJsonOptions(ConfigJson);
            services.AddScoped<TraceIPAttribute>();
            //swagger
            services.AddSwaggerGen(SwaggerConfig.ConfigSwagger);
            
            services.AddScoped<ClientIpCheckActionFilter>(container =>
            {
                return new ClientIpCheckActionFilter(Configuration["AdminSafeList"] ?? string.Empty);
            });
        }


        private static void ConfigJson(JsonOptions options)
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            options.JsonSerializerOptions.PropertyNamingPolicy = new SnakeJsonNamingPolicy();
        }

        private T Config<T>(IServiceCollection services) where T : class
        {
            var config = Activator.CreateInstance<T>();
            Configuration.Bind(typeof(T).Name, config);
            services.AddSingleton(config);
            return config;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            loggerFactory.AddLog4Net();
            app.UseRewriter(new RewriteOptions().AddRedirectToHttps());
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(CheckAllowOrigin) // allow any origin
                .AllowCredentials()); // allow credentials
            //app.UseMiddleware<AdminSafeListMiddleware>(Configuration["AdminSafeList"]);
            //keep
            //app.UseMiddleware<TokenProviderMiddleware>();
            //app.UseHttpsRedirection();
            app.UseSession();
            app.UseRouting();

            app.UseAuthentication();
            app.UseMiddleware<TokenProviderMiddleware>();
            app.UseAuthorization();

            //swagger
            app.ConfigSwagger();

            var options = new DefaultFilesOptions();
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("index.html");

            app.UseRewriter(new RewriteOptions().AddRewrite(@"^((?!.*?\b(web$.*|api\/.*)))((\w+))*\/?(\.\w{{5,}})?\??([^.]+)?$", "index.html", false));
            app.UseDefaultFiles(options);

            app.UseStaticFiles();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); endpoints.MapFallbackToFile("/index.html"); });
            //app.UseHttpsRedirection();
        }

        private bool CheckAllowOrigin(string origin)
        {
            return true;
        }
    }
}
