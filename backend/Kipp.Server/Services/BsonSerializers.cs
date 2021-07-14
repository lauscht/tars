using Kipp.Framework.Models;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Kipp.Server.Services
{
    public class BsonSerializers
    {
        public static void RegisterSerializers()
        {
            BsonClassMap.RegisterClassMap<Identity>(cm =>
            {
                cm.AutoMap();
                cm.MapIdProperty(c => c.Value).SetIdGenerator(StringObjectIdGenerator.Instance);
            });
            BsonClassMap.RegisterClassMap<Lesson>(cm =>
            {
                cm.AutoMap();
                cm.MapIdProperty(c => c.Identity);
            });
            BsonClassMap.RegisterClassMap<Course>(cm =>
            {
                cm.AutoMap();
                cm.MapIdProperty(c => c.Identity);
            });
        }
    }
}
