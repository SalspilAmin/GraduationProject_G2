using Grad_Project_G2.BLL.Services.Interfaces;
using Grad_Project_G2.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Grad_Project_G2.DAL.Data;
using Grad_Project_G2.DAL.Models;
using Grad_Project_G2.BLL.ViewModels.CourseViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Grad_Project_G2.UI.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly AppDbContext context;
        public CourseController(ICourseService cServ,AppDbContext _appDbContext)
        {
            _courseService = cServ;
            context = _appDbContext;
        }


        public IActionResult Index(int page = 1, int pageSize = 5, string? courseName = null, string? category = null)
        {
            var res = _courseService.GetCoursesWithPagination(page, pageSize, courseName, category);
            ViewBag.Category = category;
            return View(res);
        }
        public IActionResult Details(int id)
        {
            var CourseViewM=_courseService.GetCourseDetails(id);
            if (CourseViewM == null) return NotFound();
            return View(CourseViewM);
        }
        public IActionResult Create()
        {
            ViewBag.Instructors = new SelectList(
                context.Users
                       .Where(u => u.Role == UserRole.Instructor)
                       .Select(u => new
                       {
                           u.Id,
                           FullName = u.FirstName + " " + u.LastName
                       }), "Id", "FullName");
            return View(new CourseViewModel());
        }
        [HttpPost]
        public IActionResult Create(CourseViewModel courseVm)
        {
            if(ModelState.IsValid)
            {
                _courseService.CreateCourse(courseVm);
                TempData["SuccessMessage"] = "Course created successfully!";
                return RedirectToAction("Index");
            }
            ViewBag.Instructors = new SelectList(
                context.Users
                       .Where(u => u.Role == UserRole.Instructor)
                       .Select(u => new
                       {
                           u.Id,
                           FullName = u.FirstName + " " + u.LastName
                       }),"Id","FullName");
            return View(courseVm);
        }
        public IActionResult Edit(int id)
        {
            var CourseVM=_courseService.GetCourseDetails(id);
            if (CourseVM == null) return NotFound();
            ViewBag.Instructors = new SelectList(
                context.Users
                       .Where(u => u.Role == UserRole.Instructor)
                       .Select(u => new
                       {
                           u.Id,
                           FullName = u.FirstName + " " + u.LastName
                       }), "Id", "FullName"); return View(CourseVM);
        }
        [HttpPost]
        public IActionResult Edit(CourseViewModel CourseVm)
        {
            if(ModelState.IsValid)
            {
                _courseService.EditCourse(CourseVm);
                TempData["SuccessMessage"] = "Course updated successfully!";
                return RedirectToAction("Index");
            }
            ViewBag.Instructors = new SelectList(
                context.Users
                       .Where(u => u.Role == UserRole.Instructor)
                       .Select(u => new
                       {
                           u.Id,
                           FullName = u.FirstName + " " + u.LastName
                       }), "Id", "FullName"); return View(CourseVm);
        }
        public IActionResult Delete(int id)
        {
            var Course=_courseService.GetCourseDetails(id);
            if (Course == null) return NotFound();
            return View(Course);
        }
        [HttpPost,ActionName("Delete")]//to write delete asp-action <3 
        public IActionResult DeleteConfirmed(int id)
        {
            _courseService.DeleteCourse(id);
            TempData["SuccessMessage"] = "Course deleted successfully!";
            return RedirectToAction("Index");
        }
        //name is uniqe (Remote)
        public IActionResult CheckCourseName(string Name,int?Id)
        {
            if (_courseService.CourseNameExsists(Name,Id))
            {
                return Json($"Course name {Name} is Exsists");
            }
            return Json(true);
        }
    }
}
