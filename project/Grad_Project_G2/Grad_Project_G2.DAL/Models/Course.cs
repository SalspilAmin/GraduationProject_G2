using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grad_Project_G2.DAL.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required, StringLength(50, MinimumLength = 3)]
        public string Name { get; set; } = String.Empty;

        [Required]
        public string Category { get; set; } = String.Empty;
        public int InstructorId { get; set; }
        public User? Instructor { get; set; }
        public ICollection<Session>? Sessions { get; set; } = new HashSet<Session>();
    }
}
