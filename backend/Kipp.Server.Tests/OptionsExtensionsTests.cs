using System;
using System.Collections.Generic;
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
            var myConfiguration = new Dictionary<string, string>
            {
                {"GooglAuth:ClientId", "ClientId"},
                {"GooglAuth:ClientSecret", "ClientSecret"},
            };

            var configuration = new ConfigurationBuilder()
                                .AddInMemoryCollection(myConfiguration)
                                .Build();

            //act           
            services.ConfigureOption<GooglAuthOptions>(configuration);

            //assert
            var result = services.BuildServiceProvider().GetService<IOptions<GooglAuthOptions>>();

            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            Assert.NotNull(result.Value.ClientId);
            Assert.NotNull(result.Value.ClientSecret);
        }
    }
}
