using Kipp.Framework.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kipp.Framework.Services
{
    public interface IRepository<T>
    {
        /// <summary>
        /// Get a queryable list of entities.
        /// </summary>
        IQueryable<T> Get();

        /// <summary>
        /// Get a single entity.
        /// </summary>
        Task<T> Get(Identity identity);

        /// <summary>
        /// Create a new entity.
        /// </summary>
        Task Create(T entity);

        /// <summary>
        /// Put one Entity and replace it with another.
        /// </summary>
        Task Update(Identity identity, T entity);

        /// <summary>
        /// Delete a specific entity.
        /// </summary>
        Task<long> Delete(Identity identity);
    }
}
