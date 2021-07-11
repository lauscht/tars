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

        public void Create(Lesson entity) =>
            this.Lessons.InsertOne(entity);

        public long Delete(Lesson entity) =>
            this.Lessons.DeleteOne(lesson => lesson.Equals(entity)).DeletedCount;

        public IEnumerable<Lesson> Get() =>
            this.Lessons.AsQueryable();

        public void Update(Lesson entity) =>
            this.Lessons.FindOneAndReplace(lesson => lesson.Equals(entity), entity);
    }
}