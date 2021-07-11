namespace Kipp.Framework.Models{
    public class Lesson{
        /// <summary>
        /// Room where the lesson is taking place.
        /// </summary>
        public string Room {get; set;}
        /// <summary>
        /// Planned content of the lesson.
        /// </summary>
        public string Content {get; set;}
        /// <summary>
        /// Homeworks given in this lesson.
        /// </summary>
        public string Homework {get; set;}
    }
}