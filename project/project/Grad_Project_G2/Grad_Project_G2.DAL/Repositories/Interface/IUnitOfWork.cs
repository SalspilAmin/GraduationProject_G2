using Grad_Project_G2.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grad_Project_G2.DAL.Repositories.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        ICourseRepository Courses { get; }
        ISessionRepository Sessions { get; }
        IUserRepository Users { get; }
        IGradeRepository Grades { get; }

        int Save(); // SaveChanges
    }
}
