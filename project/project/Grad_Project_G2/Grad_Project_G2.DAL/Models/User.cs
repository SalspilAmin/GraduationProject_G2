using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grad_Project_G2.DAL.Models
{
    public enum UserRole
    {
        Admin,
        Instructor,
        Trainee
    }
    public class User
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string FirstName { get; set; } = string.Empty;
        [Required, StringLength(50)]
        public string LastName { get; set; } = string.Empty;
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        [Required]
        public UserRole Role { get; set; }

        public ICollection<Course>? Courses { get; set; } = new HashSet<Course>();// if Instructor
        public ICollection<Grade>? Grades { get; set; } = new HashSet<Grade>();  // if Trainee
    }
}
