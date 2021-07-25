using System.Threading.Tasks;
using Kipp.Framework.Models;
using Kipp.Server.Options;
using MongoDB.Driver;

namespace Kipp.Server.Services
{
    public class DatabaseContext : IDatabaseContext
    {
        public IMongoDatabase Database { get; protected set; }

        public DatabaseContext(DatabaseOptions options)
        {
            var client = new MongoClient(options.ConnectionString);

            if (client != null)
                this.Database = client.GetDatabase(options.DatabaseName);
        }

        public async Task<bool> Healthy() =>
            (await this.Database.ListCollectionNamesAsync()).ToList().Count > 0;

        public IMongoCollection<Lesson> Lessons =>
            this.Database.GetCollection<Lesson>(nameof(Lessons));

        public IMongoCollection<Course> Courses =>
            this.Database.GetCollection<Course>(nameof(Courses));
    }
}
