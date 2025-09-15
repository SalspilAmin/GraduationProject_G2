using Grad_Project_G2.DAL.Data;
using Grad_Project_G2.DAL.Models;
using Grad_Project_G2.DAL.Repositories.Interface;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grad_Project_G2.DAL.Repositories
{
    public class CourseRepository : GenericRepositories<Course>, ICourseRepository
    {
        private readonly AppDbContext _context;
        public CourseRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public bool isNameExsists(string name,int?id=null)
        {
            if (id == 0) return true;
            return _context.Courses.Any(c => c.Name == name && (id == null || c.Id != id));
        } 
        public string GetInsName(int id)
        {
            var ins= _context.Users.FirstOrDefault(u => u.Id == id);
            var name=(ins?.FirstName+" "+ins?.LastName)??"No instructor";
            return name;
        }

        public IEnumerable<Course> GetCoursesWithFilters(int page, int pageSize, string? courseName, string? category)
        {
            IQueryable<Course> query = _context.Courses;

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
