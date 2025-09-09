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
        public DateTime StartDate { get; set; }      
        public DateTime EndDate { get; set; }
        public int CourseId { get; set; }
        public Course? Course { get; set; }
        public ICollection<Grade>? Grades { get; set; } = new HashSet<Grade>();
    }
}
