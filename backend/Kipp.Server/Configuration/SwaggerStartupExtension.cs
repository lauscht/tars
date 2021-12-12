using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Kipp.Server.Configuration{
    public static class SwaggerStartupExtension{
        public static void AddSwaggerConfiguration(this IServiceCollection services,
            string scheme=JwtBearerDefaults.AuthenticationScheme)
        {
            services.AddSwaggerGen(c =>
            {
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = scheme,
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = scheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "KIPP API",
                    Description = "",
                    Contact = new OpenApiContact
                    {
                        Url = new Uri("https://github.com/lauscht/tars"),
                    },
                });
            });
        }

        public static void UseSwaggerWithUI(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            var servers = new List<OpenApiServer> {
                new OpenApiServer() { Url = "https://tars.lauscht.com/services/api" } 
            };
            if (env.IsDevelopment())
            {
                servers.Insert(0, new OpenApiServer()
                {
                    Url = "https://localhost:5005/"
                });
            }

            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swagger, httpReq) =>
                {
                    swagger.Servers = servers;
                });
            });

            
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = String.Empty;
                c.DocumentTitle = "Kipp - A Tars Backend";
                c.SwaggerEndpoint("swagger/v1/swagger.json", "Kipp - A Tars Backend");
            });
        }
    }
}