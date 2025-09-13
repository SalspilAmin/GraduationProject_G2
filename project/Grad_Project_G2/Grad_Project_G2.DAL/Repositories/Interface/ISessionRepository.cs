using Grad_Project_G2.DAL.Models;
using Grad_Project_G2.DAL.Repositories.Interface;

public interface ISessionRepository : IGenericRepository<Session>
{
    Task<IEnumerable<Session>> GetSessionsWithCourseAsync();
    Task<IEnumerable<Session>> SearchByCourseNameAsync(string courseName);
}

