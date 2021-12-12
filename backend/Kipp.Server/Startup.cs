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
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Kipp.Framework.Options;
using Newtonsoft.Json;
using Kipp.Server.Middlewares;
using Kipp.Server.Middlewares.Jwt;
using Kipp.Server.Configuration;

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

            services.AddScoped<IJwtValidator, JwtValidator>();
            services.AddSwaggerConfiguration();

            var authorizationOptions = Configuration.GetOptions<JwtAuthorizationOptions>();
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            
            .AddJwtBearer(options =>
            {
                options.Authority = authorizationOptions.Authority;
                options.RequireHttpsMetadata = false;


                options.SaveToken = true;
                options.Audience = authorizationOptions.Audience;
                options.ClaimsIssuer = authorizationOptions.Issuer;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authorizationOptions.Issuer,
                    ValidateIssuer = !String.IsNullOrEmpty(options.ClaimsIssuer),
                    ValidateAudience = !String.IsNullOrEmpty(options.Audience),
                    ValidTypes = new[] { "at+jwt" },
                    RequireSignedTokens = true,
                };
            });

            services.AddControllers();
            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins(
                        "http://localhost:5004",
                        "https://localhost:5001",
                        "https://localhost:5005",
                        "https://tars.lauscht.com/")
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
            }
            
            app.UseSwaggerWithUI(env);
            
            app.UseCors("default");

            app.UseHttpsRedirection();

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
