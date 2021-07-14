using Kipp.Server.Options;
using Xunit;

namespace Kipp.Server.Tests.Options
{
    public class OptionsExtensionTest
    {
        [Fact]
        public void GetSectionNameWorks()
        {
            // Arrange
            var expected = "Database";

            // Act
            var result = OptionsExtensions.GetSectionName<DatabaseOptions>();

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
