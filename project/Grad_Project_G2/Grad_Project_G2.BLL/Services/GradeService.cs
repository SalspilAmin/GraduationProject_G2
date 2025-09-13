using System;
using Grad_Project_G2.BLL.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grad_Project_G2.DAL.Models;
using Grad_Project_G2.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace Grad_Project_G2.BLL.Services
{
    public class GradeService
    {
        private readonly AppDbContext _context;

        public GradeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<GradeViewModel>> GetGradesAsync(string? search, int pageIndex, int pageSize)
        {
            var query = _context.Grades
                .Include(g => g.Trainee)
                .Include(g => g.Session)
                .Select(g => new GradeViewModel
                {
                    Id = g.Id,
                    TraineeName = g.Trainee.FirstName + " " + g.Trainee.LastName,
                    SessionName = g.Session.Name,
                    Value = g.Value
                });

            // Apply search filter if not empty
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(g => g.TraineeName.Contains(search));
            }

            return await PagedResult<GradeViewModel>.CreateAsync(query, pageIndex, pageSize);
        }


        public async Task<GradeViewModel?> GetGradeByIdAsync(int id)
        {
            return await _context.Grades
                .Include(g => g.Trainee)
                .Include(g => g.Session)
                .Where(g => g.Id == id)
                .Select(g => new GradeViewModel
                {
                    Id = g.Id,
                    Value = g.Value,
                    SessionId = g.SessionId,
                    SessionName = g.Session != null ? g.Session.Name : string.Empty,
                    TraineeId = g.TraineeId,
                    TraineeName = g.Trainee != null ? g.Trainee.FirstName : string.Empty
                })
                .FirstOrDefaultAsync();
        }

        public async Task AddGradeAsync(Grade grade)
        {
            _context.Grades.Add(grade);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGradeAsync(Grade grade)
        {
            _context.Grades.Update(grade);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGradeAsync(int id)
        {
            var grade = await _context.Grades.FindAsync(id);
            if (grade != null)
            {
                _context.Grades.Remove(grade);
                await _context.SaveChangesAsync();
            }
        }
    }
}
