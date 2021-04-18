// CRUD for Enduser Lessons

using System;
using Kipp.Framework.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kipp.Server.Controllers.Enduser{

    [ApiController]
    public class LessonController: ControllerBase {

        private ILessonRepository LessonRepository {get;}
        
        public LessonController(ILessonRepository lessonRepository) {
                this.LessonRepository = lessonRepository ?? throw new ArgumentNullException(nameof(lessonRepository));
        }

    }
}