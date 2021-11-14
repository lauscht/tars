using Kipp.Identity.Models;
using MongoDB.Driver;

namespace Kipp.Identity.Services
{
    public interface IDatabaseContext
    {
        IMongoCollection<User> Users {get;}
    }
}