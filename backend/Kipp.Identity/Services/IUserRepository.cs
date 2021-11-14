using System.Collections.Generic;
using System.Threading.Tasks;
using Kipp.Identity.Models;
using Kipp.Identity.Models.Identity;

namespace Kipp.Identity.Services
{
    public interface IUserRepository
    {
        Task<User> GetByProviderIdentity(ProviderIdentity identity);

        Task<User> Get(UserIdentity identity);

        Task Create(User entity);

        Task<long> Delete(UserIdentity identity);

        Task Update(UserIdentity identity, User entity);
    }
}