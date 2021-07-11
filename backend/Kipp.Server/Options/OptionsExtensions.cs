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

namespace Kipp.Server.Options{

    public static class OptionsExtensions{
        public static IServiceCollection ConfigureOption<T>(this IServiceCollection services, IConfiguration configuration) where T: class{
            var section_name = GetSectionName<T>();

            var section = configuration.GetSection(section_name);
            return services.Configure<T>(section);
        }

        public static string GetSectionName<T>() =>
            typeof(T).Name.Replace("Options", "");
    }
}