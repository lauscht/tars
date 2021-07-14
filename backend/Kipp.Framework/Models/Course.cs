using System.ComponentModel.DataAnnotations;

namespace Kipp.Framework.Models{
    public class Course
    { 
        public Identity Identity { get; set; }
        /// <summary>
        /// Name of the course, e.g. 9a, KS1
        /// </summary>
        [Required] public string Name { get; set; }
        /// <summary>
        /// Subject of the course, e.g. English, Arts
        /// </summary>
        [Required] public string Subject { get; set; }

        public override bool Equals(object other)
        {
            if(other is null)
            {
                return false;
            }
            else if(other is Course other_course)
            {
                return Name == other_course.Name & Subject == other_course.Subject;
            };

            return false;
        }
    }
}