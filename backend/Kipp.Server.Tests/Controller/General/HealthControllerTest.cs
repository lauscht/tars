using Kipp.Server.Controllers.General;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Kipp.Server.Tests.Controller.General
{
    public class HealthControllerTest
    {
        [Fact]
        public void ConstructorWorking()
        {
            // Act
            var result = new HealthController();

            // Assert
            Assert.IsType<HealthController>(result);
        }

        [Fact]
        public async void AliveWorking()
        {
            // Arange
            var healthController = new HealthController();

            // Act
            var result = await healthController.Alive();

            // Assert
            Assert.Equal(200, result.StatusCode);
            Assert.IsType<OkResult>(result);
        }
    }
}
