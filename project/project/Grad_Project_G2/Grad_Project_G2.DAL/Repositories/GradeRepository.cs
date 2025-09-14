using Grad_Project_G2.DAL.Data;
using Grad_Project_G2.DAL.Models;
using Grad_Project_G2.DAL.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grad_Project_G2.DAL.Repositories
{
    public class GradeRepository : GenericRepositories<Grade>, IGradeRepository
    {
        private readonly AppDbContext _context;

        public GradeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Grade>> GetGradesByTraineeAsync(int traineeId)
        {
            return await _context.Grades
                .Include(g => g.Session)
                .Include(g => g.Trainee)
                .Where(g => g.TraineeId == traineeId)
                .ToListAsync();
        }

        public async Task<Grade?> GetBySessionAndTraineeAsync(int sessionId, int traineeId)
        {
            return await _context.Grades
                .FirstOrDefaultAsync(g => g.SessionId == sessionId && g.TraineeId == traineeId);
        }
    }
}
