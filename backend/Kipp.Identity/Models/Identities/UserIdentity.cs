using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Kipp.Framework.Serializer;
using System;

namespace Kipp.Identity.Models.Identities {

    [JsonConverter(typeof(IdentityConverter<UserIdentity>))]
    [BsonSerializer(typeof(IdentitySerializer<UserIdentity>))]
    public class UserIdentity : Kipp.Framework.Models.Identity, Kipp.Framework.Models.IIdentity
    {
        public UserIdentity(): base(Guid.NewGuid().ToString()) { }
    }
}