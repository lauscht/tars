using Kipp.Framework.Models;
using MongoDB.Driver;

namespace Kipp.Server.Services
{
    public interface IDatabaseContext
    {
        IMongoCollection<Lesson> Lessons {get;}
    }
}