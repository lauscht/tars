using System;
using System.Diagnostics.CodeAnalysis;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace Kipp.Identity.Models.Identity {
        
    [BsonSerializer(typeof(UserIdentitySerializer))]
    public class UserIdentity: IEquatable<UserIdentity> {
        public string Identity {get; set;}

        [BsonConstructor]
        public UserIdentity(string identity)
        {
            Identity = identity;
        }

        public static implicit operator UserIdentity(string userIdentity) =>
            new UserIdentity(userIdentity);
        public static implicit operator string(UserIdentity userIdentity) =>
            userIdentity.Identity;

        public bool Equals([AllowNull] UserIdentity other)
        {
            if(other is null)
                return false;
            
            return other.Identity.Equals(this.Identity);
        }
    }
}