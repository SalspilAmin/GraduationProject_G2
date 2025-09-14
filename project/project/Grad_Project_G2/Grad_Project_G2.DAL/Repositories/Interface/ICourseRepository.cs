using Grad_Project_G2.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grad_Project_G2.DAL.Repositories.Interface
{
    public interface ICourseRepository:IGenericRepository<Course>
    {
        public bool isNameExsists(string name,int?id);
        public string GetInsName(int id);

        public IEnumerable<Course> GetCoursesWithFilters(int page, int pageSize, string? courseName, string? category);
        public int GetTotalCount(string? courseName, string? category);

    }
}
