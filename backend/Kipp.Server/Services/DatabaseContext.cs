using Kipp.Framework.Models;
using Kipp.Server.Options;
using MongoDB.Driver;

namespace Kipp.Server.Services
{
    public class DatabaseContext : IDatabaseContext
    {
        private IMongoDatabase Database { get; }

        public DatabaseContext(DatabaseOptions options)
        {
            var client = new MongoClient(options.ConnectionString);

            if (client != null)
                this.Database = client.GetDatabase(options.DatabaseName);
        }

        public IMongoCollection<Lesson> Lessons =>
            this.Database.GetCollection<Lesson>(nameof(Lessons));

        public IMongoCollection<Course> Courses =>
            this.Database.GetCollection<Course>(nameof(Courses));
    }
}
