using Grad_Project_G2.BLL.Validations;
using Grad_Project_G2.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grad_Project_G2.BLL.ViewModels.CourseViewModel
{
    public class EditCourseViewModel:ICourseVM
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        [NoNum]
        public string Name { get; set; } = string.Empty;
        [Required]
        public Category Category { get; set; }
        public int InstructorId { get;  set; }
    }
}
