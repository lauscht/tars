using System;
using System.Diagnostics.CodeAnalysis;

namespace Kipp.Identity.Models.Identity {
    public class ProviderIdentity: IEquatable<ProviderIdentity> {
        public string Provider {get;set;}
        public string Identity {get;set;}

        public bool Equals([AllowNull] ProviderIdentity other)
        {
            if (other is null)
                return false;
            
            if (!other.Provider.Equals(this.Provider))
                return false;
            
            return other.Identity.Equals(this.Identity);
        }
    }
}