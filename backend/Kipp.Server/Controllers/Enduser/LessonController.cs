// CRUD for Enduser Lessons

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kipp.Framework.Models;
using Kipp.Framework.Services;
using Kipp.Server.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kipp.Server.Controllers.Enduser{

    [ApiController]
    [Route("enduser/lesson")]
    [Authorize]
    public class LessonController: ControllerBase {

        private ILessonRepository LessonRepository {get;}
        
        public LessonController(ILessonRepository lessonRepository) {
                this.LessonRepository = lessonRepository ?? throw new ArgumentNullException(nameof(lessonRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lesson>>> GetAllLessons()
        {
            var identity = HttpContext.GetUserIdentity();
            var lessons = await this.LessonRepository.GetByUserIdentityAsync(identity);
            return Ok(lessons);
        }

        [HttpPost]
        public async Task<ActionResult<Lesson>> PostAsync([FromBody] Lesson lesson)
        {
            var identity = HttpContext.GetUserIdentity();
            lesson.Creator = identity;

            lesson.Identity = await this.LessonRepository.CreateAsync(lesson);
            return Ok(lesson);

        }
    }
}