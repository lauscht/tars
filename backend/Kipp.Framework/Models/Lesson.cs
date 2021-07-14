using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Kipp.Framework.Models
{
    [DebuggerDisplay("Lesson {Identity} for {Course} in {Room} starts {Start}")]
    public class Lesson 
    {
        /// <summary>
        /// Unique Identity
        /// </summary>
        public Identity Identity { get; set; }
        /// <summary>
        /// Identity of the course.
        /// </summary>
        [Required] public Course Course { get; set; }
        /// <summary>
        /// Room where the lesson is taking place.
        /// </summary>
        public string Room { get; set; }
        /// <summary>
        /// Planned content of the lesson.
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// Homeworks given in this lesson.
        /// </summary>
        public string Homework { get; set; }
        /// <summary>
        /// Date and Time when the lesson started.
        /// </summary>
        [Required] public DateTime Start { get; set; }
        /// <summary>
        /// Duration of the lesson.
        /// </summary>
        [Required] public TimeSpan Duration { get; set; }
    }
}