using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grad_Project_G2.BLL.ViewModels
{
    public class GradeViewModel
    {
        public int Id { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Grade must be between 0 and 100.")]
        public int Value { get; set; }

        [Required]
        public int SessionId { get; set; }
        [Required]
        public int TraineeId { get; set; }

        public string? SessionName { get; set; }
        public string? TraineeName { get; set; }


        public IEnumerable<SelectListItem>? Sessions { get; set; }
        public IEnumerable<SelectListItem>? Trainees { get; set; }
    }
}
