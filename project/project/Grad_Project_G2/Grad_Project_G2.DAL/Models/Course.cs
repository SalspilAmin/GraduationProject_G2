using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grad_Project_G2.DAL.Models
{
    public enum Category
    {
        Fundamentals,
        DotNet
    }
    public class Course
    {
        public int Id { get; set; }

        [Required, StringLength(50, MinimumLength = 3)]
        public string Name { get; set; } = String.Empty;

        [Required]
        public Category Category { get; set; }
        [ForeignKey("Instructor")]
        public int InstructorId { get; set; }
        public User? Instructor { get; set; }
        public ICollection<Session>? Sessions { get; set; }
    }
}
