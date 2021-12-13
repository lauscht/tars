using System;
using Kipp.Framework.Models.Identities;
using MongoDB.Bson.Serialization.Attributes;

namespace Kipp.Framework.Models{
    public class Lesson{
        [BsonId]
        public LessonIdentity Identity {get; set;}
        public UserIdentity Creator {get; set;}

    }
}