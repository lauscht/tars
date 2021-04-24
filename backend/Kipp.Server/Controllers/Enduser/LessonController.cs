// CRUD for Enduser Lessons

using System;
using System.Threading.Tasks;
using Kipp.Framework.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kipp.Server.Controllers.Enduser{

    [ApiController]
    [Route("enduser/lesson")]
    public class LessonController: ControllerBase {

        private ILessonRepository LessonRepository {get;}
        
        public LessonController(ILessonRepository lessonRepository) {
                this.LessonRepository = lessonRepository ?? throw new ArgumentNullException(nameof(lessonRepository));
        }

        [HttpGet]
        public async Task<ActionResult> GetAllLessons()
        {
            return await Task.FromResult(NoContent());
        }
    }
}