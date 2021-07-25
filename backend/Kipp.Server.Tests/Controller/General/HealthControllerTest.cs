using System;
using Kipp.Server.Controllers.General;
using Kipp.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Kipp.Server.Tests.Controller.General
{
    public class HealthControllerTest
    {
        [Fact]
        public void ConstructorFailing()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new HealthController(null);
            });
        }

        [Fact]
        public void ConstructorWorking()
        {
            // Act
            var mockDatabaseContext = new Mock<IDatabaseContext>();
            var result = new HealthController(mockDatabaseContext.Object);

            // Assert
            Assert.IsType<HealthController>(result);
        }

        [Theory]
        [InlineData(true, StatusCodes.Status200OK)]
        [InlineData(false, StatusCodes.Status503ServiceUnavailable)]
        public async void DatabaseHealthy(bool health, int expected)
        {
            // Arange
            var mockDatabaseContext = new Mock<IDatabaseContext>();
            mockDatabaseContext.Setup(c => c.Healthy()).ReturnsAsync(health);
            var healthController = new HealthController(mockDatabaseContext.Object);

            // Act
            var result = await healthController.Database();

            // Assert
            Assert.Equal(expected, result.StatusCode);
        }
    }
}
