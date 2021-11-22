using Kipp.Identity.Models;
using Kipp.Identity.Models.Identities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kipp.Identity.Services
{
    public class UserRepository : IUserRepository
    {
        private IMongoCollection<User> Users { get; }

        public UserRepository(IDatabaseContext databaseContext)
        {
            if (databaseContext is null)
                throw new ArgumentNullException(nameof(databaseContext));

            this.Users = databaseContext.Users;
        }

        public IQueryable<User> Get() =>
            Users.AsQueryable();

        public async Task<User> GetByProviderIdentity(ProviderIdentity identity) =>
            (await this.Users.FindAsync((User) => User.Identities.Equals(identity))).FirstOrDefault();

        public async Task<User> Get(UserIdentity identity) =>
            (await this.Users.FindAsync((User) => User.Identity == identity)).FirstOrDefault();

        public async Task Create(User entity) =>
            await this.Users.InsertOneAsync(entity);

        public async Task<long> Delete(UserIdentity identity) =>
            (await this.Users.DeleteOneAsync(User => User.Identity == identity)).DeletedCount;

        public async Task Update(UserIdentity identity, User entity) =>
            await this.Users.FindOneAndReplaceAsync(User => User.Identity == identity, entity);

    }
}