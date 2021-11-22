using System;
using Kipp.Framework.Models;
using Kipp.Framework.Serializer;
using Newtonsoft.Json;
using Xunit;

namespace Kipp.Framework.Tests.Serializer
{
    internal class IdentityMock{
        public Identity Identity{get; set;}
    }

    public class IdentityConverterTest{
        public IdentityConverter<Identity> Serializer {get; set;}
        public IdentityConverterTest() {
            Serializer = new IdentityConverter<Identity>();
        }

        [Theory]
        [InlineData(typeof(Identity), true)]
        [InlineData(typeof(Lesson), false)]
        public void CanConvertShouldWork(Type type, bool expected)
        {
            // Act
            var result = Serializer.CanConvert(type);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void SerializeObjectShouldWork()
        {
            // Arange
            var origin = new Identity("identity");

            var expected = "\"identity\"";

            // Act
            var result = JsonConvert.SerializeObject(origin);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void DeserializeObjectShouldWork()
        {
            // Arange
            var origin = "{\"Identity\":\"identity\"}";

            var expected = new Identity("identity");

            // Act
            var result = JsonConvert.DeserializeObject<IdentityMock>(origin);

            // Assert
            Assert.Equal(expected, result.Identity);
        }
    }
}