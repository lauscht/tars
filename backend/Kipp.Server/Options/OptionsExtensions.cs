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

namespace Kipp.Server.Options
{

    internal static class OptionsExtensions
    {
        internal static IServiceCollection ConfigureOption<T>(this IServiceCollection services, IConfiguration configuration) where T : class
        {
            var section_name = GetSectionNameFor<T>();
            return services.Configure<T>(con => 
                configuration.GetSection(section_name).Bind(con)
            );
        }

        internal static string GetSectionNameFor<T>()
        {
            var section_name = typeof(T).Name;
            return section_name.Replace("Options", "");
        }

        internal static T GetOptions<T>(this IConfiguration configuration){
                return configuration.GetSection(GetSectionNameFor<T>()).Get<T>();
        }
    }
}