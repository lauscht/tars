using Kipp.Framework.Models;
using Kipp.Server.Services;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Kipp.Server.Tests.Models
{
    public class LessonTest
    {
        public LessonTest()
        {
            BsonSerializers.RegisterSerializers();
        }

        [Fact]
        public void BsonSerializationWorks()
        {
            // Arrange
            var lesson = new Lesson()
            {
                Identity = Identity.New(),
                Course = new Course() { Name = "9a", Subject = "eng" },
                Room = "room"
            };

            // Act
            var bson = BsonExtensionMethods.ToBson(lesson);
            var result = BsonSerializer.Deserialize<Lesson>(bson);

            // Assert
            Assert.Equal(lesson.Identity, result.Identity);
            Assert.Equal(lesson.Course, result.Course);
            Assert.Equal(lesson.Room, result.Room);
        }
    }
}
