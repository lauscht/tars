// CRUD for Enduser Lessons

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kipp.Framework.Models;
using Kipp.Framework.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kipp.Server.Controllers.Enduser
{

    [ApiController]
    [Route("enduser/lesson")]
    public class LessonController : ControllerBase
    {

        private ILessonRepository LessonRepository { get; }

        public LessonController(ILessonRepository lessonRepository) {
            this.LessonRepository = lessonRepository ?? throw new ArgumentNullException(nameof(lessonRepository));
        }

        [HttpGet]
        [Route("{identity}")]
        public async Task<ActionResult<Lesson>> GetLessonAsync([FromQuery] Identity identity)
        {
            var result = LessonRepository.Get(identity);
            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        [Route("course")]
        public async Task<ActionResult<IEnumerable<Course>>> GetLessonByCourseAsync([FromBody] Course course)
        {
            var lessons = await LessonRepository.Get(course);
            return Ok(lessons);
        }

        [HttpPost]
        public async Task<ActionResult> PostLessonAsync([FromBody] Lesson lesson)
        {
            await LessonRepository.Create(lesson);

            return Created(lesson.Identity.Value, lesson);
        }

        [HttpPut]
        public async Task<ActionResult> PostLessonAsync([FromQuery] Identity identity, [FromBody] Lesson lesson)
        {
            await LessonRepository.Update(identity, lesson);

            return Ok(lesson);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteLessonAsync([FromQuery] Identity identity)
        {
            var deleted = await LessonRepository.Delete(identity);
            if (deleted == 0)
                return NotFound();

            return Ok();
        }
    }
}