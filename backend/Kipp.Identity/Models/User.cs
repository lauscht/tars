
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Kipp.Identity.Models.Identity;
using MongoDB.Bson.Serialization.Attributes;

namespace Kipp.Identity.Models
{
    public class User
    {
        [BsonId]
        public UserIdentity Identity { get; set; }

        public string Username {get; set;}
        public List<ProviderIdentity> Identities { get; set; }
        public DateTime Created { get; protected set; }
        public DateTime LastLogin { get; set; }

        public static User Create(ProviderIdentity providerIdentity)
        {
            return new User()
            {
                Identities = new List<ProviderIdentity>() {providerIdentity,},
                Created = DateTime.Now,
            };
        }
    }
}