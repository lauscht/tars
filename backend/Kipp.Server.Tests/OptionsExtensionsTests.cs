using System;
using System.Collections.Generic;
using Kipp.Framework.Options;
using Kipp.Server.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace Kipp.Server.Tests
{
    public class OptionsExtensionsTests
    {
        [Fact(DisplayName = "Test that we can read the configuration.")]
        public void ConfigureOption01()
        {
            //arrange
            var services = new ServiceCollection();            
            var configuration = this.SetupConfiguration();

            //act           
            services.ConfigureOption<DatabaseOptions>(configuration);

            //assert
            var result = services.BuildServiceProvider().GetService<IOptions<DatabaseOptions>>();

            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            Assert.NotNull(result.Value.DatabaseName);
            Assert.NotNull(result.Value.ConnectionString);
        }

        [Fact(DisplayName = "Test that we can read the configuration.")]
        public void GetOptions01()
        {
            //arrange
            var configuration = this.SetupConfiguration();

            //act
            var result = configuration.GetOptions<DatabaseOptions>();

            //assert
            Assert.NotNull(result);
            Assert.NotNull(result.DatabaseName);
            Assert.NotNull(result.ConnectionString);
        }

        private IConfiguration SetupConfiguration()
        {
            var myConfiguration = new Dictionary<string, string>
            {
                {"Database:DatabaseName", "DatabaseName"},
                {"Database:ConnectionString", "ConnectionString"},
            };

            var configuration = new ConfigurationBuilder()
                                .AddInMemoryCollection(myConfiguration)
                                .Build();

            return configuration;
        }
    }
}
