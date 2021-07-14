using Kipp.Framework.Models;
using MongoDB.Driver;

namespace Kipp.Server.Services
{
    public interface IDatabaseContext
    {
        IMongoCollection<Course> Courses { get; }
        IMongoCollection<Lesson> Lessons { get; }
    }
}