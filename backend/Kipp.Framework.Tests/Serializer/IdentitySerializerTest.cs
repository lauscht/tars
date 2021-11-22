using System;
using System.IO;
using Kipp.Framework.Models;
using Kipp.Framework.Serializer;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using Xunit;

namespace Kipp.Framework.Tests.Serializer
{
    public class IdentitySerializerTest{
        public IdentitySerializer<Identity> Serializer {get; set;}
        public IdentitySerializerTest() {
            Serializer = new IdentitySerializer<Identity>();
        }

        [Theory]
        [InlineData(typeof(Identity), true)]
        [InlineData(typeof(Lesson), false)]
        public void CanConvertShouldWork(Type type, bool expected)
        {
            // Act
            var result = Serializer.GetType() == type;

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void DeserializeShouldWork()
        {
            // Arange
            var origin = "\"identity\"";
            var expected = new Identity("identity");
            var reader = new JsonReader(origin);
            var context = BsonDeserializationContext.CreateRoot(reader);

            // Act
            var result = Serializer.Deserialize(context) as Identity;

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void DeserializeObjectShouldWork()
        {
            // Arange
            var expected = "\"identity\"";
            var origin = new Identity("identity");

            var stringWriter = new StringWriter();
            var writer = new JsonWriter(stringWriter);
            var context = BsonSerializationContext.CreateRoot(writer);
            var args = new BsonSerializationArgs();

            // Act
            Serializer.Serialize(context, args, origin);
            var result = stringWriter.ToString();


            // Assert
            Assert.Equal(expected, result);
        }
    }
}