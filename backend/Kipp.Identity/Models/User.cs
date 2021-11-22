
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Kipp.Identity.Models.Identities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace Kipp.Identity.Models
{
    public class User
    {
        [BsonId]
        public UserIdentity Identity { get; set; }

        public string Username {get; set;}
        public string Email {get; set;}
        public List<ProviderIdentity> Identities { get; set; }
        public DateTime Created { get; protected set; }
        public DateTime LastLogin { get; set; }

        public static User Create(ProviderIdentity providerIdentity)
        {
            return new User()
            {
                Identity = new UserIdentity(),
                Identities = new List<ProviderIdentity>() {providerIdentity,},
                Created = DateTime.Now,
            };
        }
    }
}