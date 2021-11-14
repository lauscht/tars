using System;
using System.Diagnostics.CodeAnalysis;

namespace Kipp.Identity.Models.Identity {
    public class UserIdentity: IEquatable<UserIdentity> {
        public string Identity {get; set;}


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