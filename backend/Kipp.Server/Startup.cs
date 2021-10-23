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
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Kipp.Server
{
    public class Startup
    {
        private const string GoogleAuthenticationScheme = "GoogleAuthenticationScheme";
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
            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie(options =>
                {
                    options.LoginPath = "/auth/google-login"; // Must be lowercase
                })
                .AddGoogle(options =>
                {
                    var googleAuthNSection = this.Configuration.GetOptions<GoogleAuthOptions>();
                    
                    options.ClientId = googleAuthNSection.ClientId;
                    options.ClientSecret = googleAuthNSection.ClientSecret;
                    options.CallbackPath = "/auth/google-callback";
                });
            services.AddAuthorization();

            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins("https://localhost:5001", "https://tars.lauscht.com/")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
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
                        swagger.Servers = new List<OpenApiServer> { new OpenApiServer { Url = "https://tars.lauscht.com/services/api" } };
                    });
                });
            };
            
            app.UseCors("default");
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = String.Empty;
                c.DocumentTitle = "Kipp - A Tars Backend";
                c.SwaggerEndpoint("swagger/v1/swagger.json", "Kipp - A Tars Backend");
            });

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(c =>
            {
                c.MapControllers();
            });
        }
    }
}
