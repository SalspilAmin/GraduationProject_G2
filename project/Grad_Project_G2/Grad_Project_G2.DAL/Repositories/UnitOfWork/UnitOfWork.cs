using Grad_Project_G2.DAL.Data;
using Grad_Project_G2.DAL.Models;
using Grad_Project_G2.DAL.Repositories.Interface;
using Grad_Project_G2.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grad_Project_G2.DAL.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;


        private ICourseRepository? courses;
        private ISessionRepository? sessions;
        private IUserRepository? users;
        private IGradeRepository? grades;

        public UnitOfWork(AppDbContext context)
        {
            this.context = context;


        }

        public ICourseRepository Courses => courses ??  new CourseRepository(context);

        public ISessionRepository Sessions => sessions ??= new SessionRepository(context);

        public IUserRepository Users => users ??= new UserRepository(context);

        public IGradeRepository Grades => grades ??= new GradeRepository(context);







        public int Save()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }



    }
}
