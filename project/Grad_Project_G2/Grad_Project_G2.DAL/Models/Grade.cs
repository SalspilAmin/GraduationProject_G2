using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grad_Project_G2.DAL.Models
{
    public class Grade
    {
        public int Id { get; set; }
        [Range(0, 100)]
        public int Value { get; set; }
        [Required]
        public int SessionId { get; set; }
        [Required]
        public int TraineeId { get; set; }
        public Session? Session { get; set; }
        public User? Trainee { get; set; }
    }
}
