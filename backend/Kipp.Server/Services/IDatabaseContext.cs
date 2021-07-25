using Kipp.Framework.Models;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Kipp.Server.Services
{
    public interface IDatabaseContext
    {
        IMongoDatabase Database { get; }
        IMongoCollection<Course> Courses { get; }
        IMongoCollection<Lesson> Lessons { get; }

        Task<bool> Healthy();
    }
}