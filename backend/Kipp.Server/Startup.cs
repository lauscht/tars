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
            services.ConfigureOption<TarsAuthenticationOptions>(Configuration);

            services.AddSwaggerGen(c =>
            {
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
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

            var authenticationOptions = Configuration.GetOptions<TarsAuthenticationOptions>();
            var authenticationSecret = Encoding.ASCII.GetBytes(authenticationOptions.ClientSecret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.Authority = "https://localhost:5003";
                options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
                /*
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = false,
                    //IssuerSigningKey = new SymmetricSecurityKey(authenticationSecret),
                    ValidateIssuer = false,
                    //ValidIssuer = authenticationOptions.Issuer,
                    ValidateAudience = false,
                    //ValidAudience = authenticationOptions.Audience
                };*/ 
            });

            services.AddControllers();
            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins("http://localhost:5004", "https://localhost:5005",
                        "https://localhost:5001", "https://tars.lauscht.com/")
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
            
            // app.UseMiddleware<JwtMiddleware>();
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
