using System;
using System.Diagnostics.CodeAnalysis;
using Kipp.Framework.Serializer;
using Newtonsoft.Json;

namespace Kipp.Framework.Models {

    [JsonConverter(typeof(IdentityConverter<Identity>))]
    public class Identity : IIdentity, IEquatable<Identity>, IEquatable<IIdentity>
    {
        public Identity() { }
        public Identity(string value) {
            Value = value;
        }

        public string Value { get; set; }

        public bool Equals(IIdentity other)
        {
            return this.Value.Equals(other.Value);
        }

        public bool Equals([AllowNull] Identity other)
        {
            if (other is null)
                return false;

            return Value.Equals(other.Value);
        }

        public static implicit operator string(Identity identity) =>
            identity.Value;
    }
}