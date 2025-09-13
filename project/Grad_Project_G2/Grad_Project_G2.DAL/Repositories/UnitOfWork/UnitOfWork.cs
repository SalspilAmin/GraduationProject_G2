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
        private ISessionRepository? sessionRepository;


        private IGenericRepository<Course>? courses;
        private IGenericRepository<Session>? sessions;
        private IUserRepository? users;
        private IGenericRepository<Grade>? grades;

        public UnitOfWork(AppDbContext context)
        {
            this.context = context;


        }

        public ICourseRepository Courses => (ICourseRepository)(courses ??  new CourseRepository(context));

        public ISessionRepository Sessions => sessionRepository ??= new SessionRepository(context);

        public IUserRepository Users => users ??= new UserRepository(context);

        public IGradeRepository Grades => throw new NotImplementedException();







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
