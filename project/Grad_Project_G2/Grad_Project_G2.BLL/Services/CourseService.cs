using Grad_Project_G2.BLL.Services.Interfaces;
using Grad_Project_G2.BLL.ViewModels;
using Grad_Project_G2.BLL.ViewModels.CourseViewModel;
using Grad_Project_G2.DAL.Models;
using Grad_Project_G2.DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace Grad_Project_G2.BLL.Services
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CourseService(IUnitOfWork UOW)
        {
            _unitOfWork = UOW;
        }
        public bool CourseNameExsists(string name,int?id)
        {
            return(_unitOfWork.Courses.isNameExsists(name,id));
        }

        public void CreateCourse(CourseViewModel vm)
        {
            var course = new Course
            {
                Name = vm.Name,
                Category = vm.Category,
                InstructorId = vm.InstructorId
            };
            

            _unitOfWork.Courses.Add(course);
            _unitOfWork.Save();
        }

        public void DeleteCourse(int id)
        {
            _unitOfWork.Courses.Delete(id);
            _unitOfWork.Save();
        }

        public void EditCourse(CourseViewModel vm)
        {
            var course = _unitOfWork.Courses.GetById(vm.Id);
            if (course == null) return;
            course.Name = vm.Name;
            course.Category = vm.Category;
            course.InstructorId = vm.InstructorId;
            _unitOfWork.Save();
        }

        public CourseViewModel GetCourseDetails(int id)
        {
            var course = _unitOfWork.Courses.GetById(id);
            if (course == null) return null;

            return new CourseViewModel
            {
                Id = course.Id,
                Name = course.Name,

                Category = course.Category,

                InstructorName = _unitOfWork.Courses.GetInsName(course.InstructorId)
            };
        }

        public EditCourseViewModel GetCourseForEdit(int id)
        {
            var course = _unitOfWork.Courses.GetById(id);
            if (course == null) return null;

            return new EditCourseViewModel
            {
                Id = course.Id,
                Name = course.Name,
                Category = course.Category,
                InstructorId = course.InstructorId
            };
        }

        public PagedResult<CourseViewModel> GetCoursesWithPagination(
            int page, int pageSize, string? courseName = null, string? category = null)
        {
            var courses = _unitOfWork.Courses
                .GetCoursesWithFilters(page, pageSize, courseName, category)
                .Select(c => new CourseViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Category = c.Category,
                    InstructorName = _unitOfWork.Courses.GetInsName(c.InstructorId)
                })
                .ToList();

            return new PagedResult<CourseViewModel>
            {
                Items = courses,
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = _unitOfWork.Courses.GetTotalCount(courseName, category)
            };
        }



    }
}
