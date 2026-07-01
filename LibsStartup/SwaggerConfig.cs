using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace QlThietBi.LibsStartup
{
    public static class SwaggerConfig
    {
        public static void ConfigSwagger(this SwaggerGenOptions genOption)
        {
            genOption.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "QLTB API",
                Description = "API Quản lý thiết bị - mô tả các luồng nghiệp vụ và dữ liệu thiết bị.",
                Contact = new OpenApiContact()
                {
                    Email = "pthanhdng@gmail.com",
                    Name = "Pham Ngoc Thanh",
                    Url = new Uri("https://dawaco.com.vn/trung-tam-cntt-dawaco/")
                },
                Version = "v1"
            });
            genOption.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            genOption.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,

                    },
                    new List<string>()
                }
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
            {
                genOption.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
            }
        }
        public static void ConfigSwagger(this IApplicationBuilder app)
        {
            app.UseSwaggerUI(x =>
            {
                x.ConfigObject.Urls = new[]
                {
                    new UrlDescriptor
                    {
                        Name = "Localhost",
                        Url = "/swagger/v1/swagger.json"
                    }
                };
                x.DocumentTitle = "QlThietBi API Documentation";
                x.RoutePrefix = string.Empty;
                x.DisplayRequestDuration();
                x.DefaultModelsExpandDepth(-1);
            });

            app.UseSwagger();
        }
    }
}
