using Kipp.Framework.Services;
using Kipp.Server.Options;
using Kipp.Server.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace Kipp.Server
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureOption<DatabaseOptions>(Configuration);
            services.ConfigureOption<GoogleAuthOptions>(Configuration);
            
            services.AddSwaggerGen(c =>
            {
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
            services.AddControllers();

            // Repositories
            services.AddSingleton<IDatabaseContext, DatabaseContext>();
            services.AddSingleton<ILessonRepository, LessonRepository>();

            // Database BSON Map
            BsonSerializers.RegisterSerializers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();

            }else
            {
                app.UseSwagger(c =>
                {
                    c.PreSerializeFilters.Add((swagger, httpReq) =>
                    {
                        swagger.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"https://tars.lauscht.com/api" } };
                    });
                });
            };
            
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("swagger/v1/swagger.json", "Kipp - A Tars Backend");
                c.RoutePrefix = String.Empty;
            });

            app.UseRouting();
            app.UseEndpoints(c =>
            {
                c.MapControllers();
            });
        }
    }
}
