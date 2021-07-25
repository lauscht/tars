using System;
using System.Threading.Tasks;
using Kipp.Framework.Models;
using Kipp.Server.Options;
using MongoDB.Driver;

namespace Kipp.Server.Services
{
    public class DatabaseContext : IDatabaseContext
    {
        public IMongoDatabase Database { get; protected set; }

        public DatabaseContext(DatabaseOptions databaseOptions)
        {
            if (databaseOptions is null)
                throw new ArgumentNullException(nameof(databaseOptions));

            var mongoClient = new MongoClient(databaseOptions.ConnectionString);

            if (mongoClient is null)
                throw new ArgumentNullException(nameof(mongoClient));

            this.Database = mongoClient.GetDatabase(databaseOptions.DatabaseName);
        }

        public async Task<bool> Healthy() =>
            (await this.Database.ListCollectionNamesAsync()).ToList().Count > 0;

        public IMongoCollection<Lesson> Lessons =>
            this.Database.GetCollection<Lesson>(nameof(Lessons));

        public IMongoCollection<Course> Courses =>
            this.Database.GetCollection<Course>(nameof(Courses));
    }
}
