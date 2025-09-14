using Grad_Project_G2.BLL.Services.Interfaces;
using Grad_Project_G2.BLL.ViewModels;
using Grad_Project_G2.DAL.Data;
using Grad_Project_G2.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Grad_Project_G2.BLL.Services
{
    public class GradeService : IGradeService
    {
        private readonly AppDbContext _context;

        public GradeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<GradeViewModel>> GetPagedGradesAsync(int pageNumber, int pageSize, string search)
        {
            var query = _context.Grades
                .Include(g => g.Trainee)
                .Include(g => g.Session)
                .Select(g => new GradeViewModel
                {
                    Id = g.Id,
                    TraineeName = g.Trainee.FirstName + " " + g.Trainee.LastName,
                    Value = g.Value,
                    SessionId = g.SessionId,
                    TraineeId = g.TraineeId
                });

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(g => g.TraineeName.Contains(search));

            return await PagedResult<GradeViewModel>.CreateAsync(query, pageNumber, pageSize);
        }

        public async Task<GradeViewModel> PrepareGradeViewModel(GradeViewModel? model = null)
        {
            return await Task.FromResult(model ?? new GradeViewModel());
        }

        public async Task CreateGradeAsync(GradeViewModel model)
        {
            var grade = new Grade
            {
                TraineeId = model.TraineeId,
                SessionId = model.SessionId,
                Value = model.Value
            };

            _context.Grades.Add(grade);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGradeAsync(GradeViewModel model)
        {
            var grade = await _context.Grades.FindAsync(model.Id);
            if (grade == null) return;

            grade.TraineeId = model.TraineeId;
            grade.SessionId = model.SessionId;
            grade.Value = model.Value;

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

        public async Task<GradeViewModel?> GetByIdAsync(int id)
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
                    TraineeId = g.TraineeId,
                    TraineeName = g.Trainee != null ? g.Trainee.FirstName + " " + g.Trainee.LastName : string.Empty
                })
                .FirstOrDefaultAsync();
        }
    }
}
