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

    internal static class OptionsExtensions{
        internal static IServiceCollection ConfigureOption<T>(this IServiceCollection services, IConfiguration configuration) where T: class{
            var section_name = typeof(T).Name;
            section_name = section_name.Replace("Options", "");

            var section = configuration.GetSection(section_name);
            return services.Configure<T>(section);
        }
    }
}