using Grad_Project_G2.DAL.Data;
using Grad_Project_G2.DAL.Models;
using Grad_Project_G2.DAL.Repositories;
using Grad_Project_G2.DAL.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

public class SessionRepository : GenericRepositories<Session>, ISessionRepository
{
    private readonly AppDbContext _context;

    public SessionRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Session>> GetSessionsWithCourseAsync()
    {
        return await _context.Sessions
            .Include(s => s.Course)
            .ToListAsync();


    }

    public async Task<IEnumerable<Session>> SearchByCourseNameAsync(string courseName)
    {
        return await _context.Sessions
            .Include(s => s.Course)
            .Where(s => s.Course.Name.Contains(courseName))
            .ToListAsync();

    }
}
