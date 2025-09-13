using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grad_Project_G2.DAL.Models
{
    public class Session
    {
        public int Id { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]

        public int CourseId { get; set; }
        [Required]
        public string Name { get; set; }
        public Course? Course { get; set; }
        public ICollection<Grade>? Grades { get; set; } = new HashSet<Grade>();
    }
}
