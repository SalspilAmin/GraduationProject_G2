using Grad_Project_G2.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grad_Project_G2.BLL.ViewModels.CourseViewModel
{
    public interface ICourseVM
    {
        string Name { get; set; }
        Category Category { get; set; }
        int InstructorId { get; set; }
    }
}
