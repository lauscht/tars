using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Kipp.Framework.Serializer;
using System;

namespace Kipp.Framework.Models.Identities {

    [JsonConverter(typeof(IdentityConverter<LessonIdentity>))]
    [BsonSerializer(typeof(IdentitySerializer<LessonIdentity>))]
    public class LessonIdentity : Kipp.Framework.Models.Identity, Kipp.Framework.Models.IIdentity
    {
        public LessonIdentity(): base(Guid.NewGuid().ToString()) { }
    }
}