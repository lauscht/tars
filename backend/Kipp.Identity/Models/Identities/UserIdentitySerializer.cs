using System;
using System.Diagnostics.CodeAnalysis;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace Kipp.Identity.Models.Identity {
        
    public class UserIdentitySerializer : IBsonSerializer 
    {
        public Type ValueType => typeof(System.String);

        public object Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var identity = context.Reader.ReadString();
            return new UserIdentity(identity);
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            if (value is UserIdentity identity)
            {
                context.Writer.WriteString(identity.Identity);
            }
        }
    }
}