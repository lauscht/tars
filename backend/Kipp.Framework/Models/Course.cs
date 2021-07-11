using System.ComponentModel.DataAnnotations;

namespace Kipp.Framework.Models{
    public class Course
    { 
        /// <summary>
        /// Name of the course, e.g. 9a, KS1
        /// </summary>
        [Required] public string Name { get; set; }
        /// <summary>
        /// Subject of the course, e.g. English, Arts
        /// </summary>
        [Required] public string Subject { get; set; }
    }
}