using System.Collections.Generic;
using System.Threading.Tasks;
using Kipp.Framework.Models;
using Kipp.Framework.Models.Identities;

namespace Kipp.Framework.Services{
    public interface ILessonRepository
    {
        Task<IEnumerable<Lesson>> GetByUserIdentityAsync(UserIdentity name);
        Task<LessonIdentity> CreateAsync(Lesson lesson);
    }
}