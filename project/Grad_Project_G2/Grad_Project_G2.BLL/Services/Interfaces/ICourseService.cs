using Grad_Project_G2.BLL.ViewModels;
using Grad_Project_G2.BLL.ViewModels.CourseViewModel;
using Grad_Project_G2.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Grad_Project_G2.BLL.Services.Interfaces
{
    public interface ICourseService
    {
        public PagedResult<CourseViewModel> GetCoursesWithPagination(int page, int pageSize,
       string courseName=null , string category=null );
        CourseViewModel GetCourseDetails(int id);
        void CreateCourse(CourseViewModel vm);
        EditCourseViewModel GetCourseForEdit(int id);
        void EditCourse(CourseViewModel vm);
        void DeleteCourse(int id);
        bool CourseNameExsists(string name,int?id);
    }
}
