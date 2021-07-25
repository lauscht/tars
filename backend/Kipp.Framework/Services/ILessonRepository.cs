using Kipp.Framework.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kipp.Framework.Services
{
    public interface ILessonRepository: IRepository<Lesson>
    {
        Task<IEnumerable<Lesson>> Get(Course course);
    }
}