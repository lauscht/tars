
using System;
using Kipp.Framework.Models;
using MongoDB.Bson.Serialization;

namespace Kipp.Framework.Serializer
{
    public class IdentitySerializer<T> : IBsonSerializer where T: IIdentity
    {
        public Type ValueType => typeof(T);

        public object Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var value = context.Reader.ReadString();
            var identity = Activator.CreateInstance<T>();
            identity.Value = value;
            return identity;
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            if (value is T identity)
            {
                context.Writer.WriteString(identity.Value);
            }
        }
    }
}