using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kipp.Framework.Models;
using Kipp.Framework.Models.Identities;
using Kipp.Framework.Services;
using MongoDB.Driver;

namespace Kipp.Server.Services{
    public class LessonRepository : ILessonRepository
    {
        private IMongoCollection<Lesson> Lessons { get; }

        public LessonRepository(IDatabaseContext databaseContext)
        {
            if (databaseContext is null)
                throw new ArgumentNullException(nameof(databaseContext));

            this.Lessons = databaseContext.Lessons;
        }
        public async Task<IEnumerable<Lesson>> GetByUserIdentityAsync(UserIdentity identity) =>
            (await this.Lessons.FindAsync(dbo => dbo.Creator == identity)).ToEnumerable();

        public async Task<LessonIdentity> CreateAsync(Lesson lesson)
        {
            lesson.Identity = new LessonIdentity();
            await this.Lessons.InsertOneAsync(lesson);
            return lesson.Identity;
        }
    }
}