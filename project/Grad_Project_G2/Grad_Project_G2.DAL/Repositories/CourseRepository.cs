using Grad_Project_G2.DAL.Data;
using Grad_Project_G2.DAL.Models;
using Grad_Project_G2.DAL.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Grad_Project_G2.DAL.Repositories
{
    public class CourseRepository : GenericRepositories<Course>, ICourseRepository
    {
        private readonly AppDbContext _context;

        public CourseRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        // تحقق من وجود اسم الكورس مسبقًا
        public bool isNameExsists(string name, int? id = null)
        {
            return _context.Courses.Any(c => c.Name == name && (id == null || c.Id != id));
        }

        // بدل ما نعمل استعلام منفصل لكل Instructor
        // هنستخدم Include مرة واحدة
        public string GetInsName(int id)
        {
            var ins = _context.Users
                .AsNoTracking()
                .FirstOrDefault(u => u.Id == id);

            return (ins != null ? ins.FirstName + " " + ins.LastName : "No instructor");
        }

        public IEnumerable<Course> GetCoursesWithFilters(int page, int pageSize, string? courseName, string? category)
        {
            IQueryable<Course> query = _context.Courses
                .Include(c => c.Instructor) // 🟢 هنا هنجيب الـ Instructor مع الكورس
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(courseName))
                query = query.Where(c => c.Name.Contains(courseName));

            if (!string.IsNullOrWhiteSpace(category) &&
                Enum.TryParse<Category>(category, out var parsedCategory))
                query = query.Where(c => c.Category == parsedCategory);

            return query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public int GetTotalCount(string? courseName, string? category)
        {
            IQueryable<Course> query = _context.Courses;

            if (!string.IsNullOrWhiteSpace(courseName))
                query = query.Where(c => c.Name.Contains(courseName));

            if (!string.IsNullOrWhiteSpace(category) &&
                Enum.TryParse<Category>(category, out var parsedCategory))
                query = query.Where(c => c.Category == parsedCategory);

            return query.Count();
        }
    }
}
