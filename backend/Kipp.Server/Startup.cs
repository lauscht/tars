using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Kipp.Server.Options;
using Kipp.Server.Services;
using Kipp.Framework.Services;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

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
            services.ConfigureOption<GooglAuthOptions>(Configuration);

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
            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    var googleAuthNSection = this.Configuration.GetOptions<GooglAuthOptions>();
                    options.ClientId = googleAuthNSection.ClientId;
                    options.ClientSecret = googleAuthNSection.ClientSecret;
                });

            // Repositories
            services.AddSingleton<ILessonRepository, LessonRepository>();
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
                        swagger.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpReq.Scheme}://tars.lauscht.com/api" } };
                    });
                });
            };
            
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("swagger/v1/swagger.json", "Kipp - A Tars Backend");
                c.RoutePrefix = String.Empty;
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseRouting();
            app.UseEndpoints(c =>
            {
                c.MapControllers();
            });
        }
    }
}
