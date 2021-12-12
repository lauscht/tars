// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Security.Cryptography.X509Certificates;
using System.Text;
using IdentityServer4;
using Kipp.Framework.Options;
using Kipp.Identity.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson.Serialization;

namespace Kipp.Identity
{
    public class Startup
    {
        private string PathBase {get; set;}
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {            
            services.Configure<DatabaseOptions>(Configuration.GetSection("Database"));
            
            PathBase = this.Configuration.GetValue<string>(nameof(PathBase));

            var config = new Config();
            services.AddControllersWithViews();
            var builder = services.AddIdentityServer(options =>
            {
                // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                options.EmitStaticAudienceClaim = true;
                
            })
                .AddInMemoryIdentityResources(config.IdentityResources)
                .AddInMemoryApiResources(config.ApiResources)
                .AddInMemoryApiScopes(config.ApiScopes)
                .AddInMemoryClients(config.Clients);

            var certificateOptions = Configuration.GetSection("Certificate").Get<CertificateOptions>();
            var cert = new X509Certificate2(certificateOptions.Filename, certificateOptions.Key, X509KeyStorageFlags.Exportable);
            // not recommended for production - you need to store your key material somewhere secure
            // builder.AddDeveloperSigningCredential();
            builder.AddSigningCredential(cert)
                   .AddValidationKey(cert);
            // configures the OpenIdConnect handlers to persist the state parameter into the server-side IDistributedCache.
            services.AddOidcStateDataFormatterCache();

            var googleAuth = Configuration.GetSection("GoogleAuth").Get<GoogleAuthOptions>();
            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                    options.ClientId = googleAuth.ClientId;
                    options.ClientSecret = googleAuth.ClientSecret;
                });

            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins(
                            "https://localhost:5001",
                            "https://tars.lauscht.com/"
                            )
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddSingleton<IDatabaseContext, DatabaseContext>();
            services.AddSingleton<IUserRepository, UserRepository>();
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UsePathBase(PathBase);

            app.UseCors("default");
            //app.UseStaticFiles();
            app.UseRouting();
            //app.UseHttpsRedirection();

            app.UseIdentityServer();            
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
               endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
