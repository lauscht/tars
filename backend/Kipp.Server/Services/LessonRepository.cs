using Kipp.Framework.Models;
using Kipp.Framework.Services;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kipp.Server.Services
{
    public class LessonRepository : ILessonRepository
    {
        private IMongoCollection<Lesson> Lessons { get; }

        public LessonRepository(DatabaseContext databaseContext)
        {
            if (databaseContext is null)
                throw new ArgumentNullException(nameof(databaseContext));

            this.Lessons = databaseContext.Lessons;
        }

        public async Task<IEnumerable<Lesson>> Get(Course course) =>
            (await this.Lessons.FindAsync((lesson) => lesson.Course.Equals(course))).ToEnumerable();

        public async Task<Lesson> Get(Identity identity) =>
            (await this.Lessons.FindAsync((lesson) => lesson.Identity.Value == identity.Value)).FirstOrDefault();

        public async Task Create(Lesson entity) =>
            await this.Lessons.InsertOneAsync(entity);

        public async Task<long> Delete(Identity identity) =>
            (await this.Lessons.DeleteOneAsync(lesson => lesson.Identity.Value == identity.Value)).DeletedCount;

        public async Task Update(Identity identity, Lesson entity) =>
            await this.Lessons.FindOneAndReplaceAsync(lesson => lesson.Identity.Value == identity.Value, entity);
    }
}