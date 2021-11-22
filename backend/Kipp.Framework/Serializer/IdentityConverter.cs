using System;
using Kipp.Framework.Models;
using Newtonsoft.Json;

namespace Kipp.Framework.Serializer
{
    public class IdentityConverter<T>: JsonConverter where T: IIdentity, new() {

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {

            if (value is IIdentity identity)
            {
                if (identity is null)
                    throw new ArgumentNullException(nameof(identity));

                writer.WriteValue(identity.Value);
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = serializer.Deserialize<string>(reader);
            var identity = new T();
            identity.Value = value;
            return identity;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(T);
        }
    }
}