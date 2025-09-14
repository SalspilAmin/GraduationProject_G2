using Grad_Project_G2.DAL.Models;
using Grad_Project_G2.BLL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Grad_Project_G2.DAL.Data;

namespace Grad_Project_G2.Controllers
{
    public class GradeController : Controller
    {
        private readonly AppDbContext _context;

        public GradeController(AppDbContext context)
        {
            _context = context;
        }

        // ========================
        // Index
        // ========================
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var grades = await _context.Grades
                .Include(g => g.Session)
                .Include(g => g.Trainee)
                .ToListAsync();

            var gradeViewModels = grades.Select(g => new GradeViewModel
            {
                Id = g.Id,
                Value = g.Value,
                SessionId = g.SessionId,
                TraineeId = g.TraineeId,
                SessionName = g.Session?.Name,
                TraineeName = g.Trainee?.FirstName
            }).ToList();

            var pagedResult = new PagedResult<GradeViewModel>
            {
                Items = gradeViewModels.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                TotalItems = gradeViewModels.Count,
                PageNumber = page,
                PageSize = pageSize
            };

            return View(pagedResult);
        }
        // ========================
        // Details
        // ========================
        public async Task<IActionResult> Details(int id)
        {
            var grade = await _context.Grades
                .Include(g => g.Session)
                .Include(g => g.Trainee)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (grade == null) return NotFound();

            var model = new GradeViewModel
            {
                Id = grade.Id,
                Value = grade.Value,
                SessionId = grade.SessionId,
                TraineeId = grade.TraineeId,
                SessionName = grade.Session?.Name,
                TraineeName = grade.Trainee?.FirstName
            };

            return View(model);
        }

        // ========================
        // Create GET
        // ========================
        public IActionResult Create()
        {

            var model = new GradeViewModel
            {
                Sessions = _context.Sessions
                    .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name })
                    .ToList(),
                Trainees = _context.Users
                    .Where(u => u.Role == UserRole.Trainee)
                    .Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.FirstName })
                    .ToList()
            };

            return View(model);
        }

        // ========================
        // Create POST
        // ========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GradeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var grade = new Grade
                {
                    Value = model.Value,
                    SessionId = model.SessionId,
                    TraineeId = model.TraineeId
                };

                _context.Grades.Add(grade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            model.Sessions = _context.Sessions
                .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name })
                .ToList();
            model.Trainees = _context.Users
                .Where(u => u.Role == UserRole.Trainee)
                .Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.FirstName })
                .ToList();

            return View(model);
        }

        // ========================
        // Edit GET
        // ========================
        public async Task<IActionResult> Edit(int id)
        {
            var grade = await _context.Grades.FindAsync(id);
            if (grade == null) return NotFound();

            var model = new GradeViewModel
            {
                Id = grade.Id,
                Value = grade.Value,
                SessionId = grade.SessionId,
                TraineeId = grade.TraineeId,
                Sessions = _context.Sessions
                    .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name })
                    .ToList(),
                Trainees = _context.Users
                    .Where(u => u.Role == UserRole.Trainee)
                    .Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.FirstName })
                    .ToList()
            };

            return View(model);
        }

        // ========================
        // Edit POST
        // ========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GradeViewModel model)
        {
            if (id != model.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var grade = await _context.Grades.FindAsync(id);
                    if (grade == null) return NotFound();

                    grade.Value = model.Value;
                    grade.SessionId = model.SessionId;
                    grade.TraineeId = model.TraineeId;

                    _context.Update(grade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Grades.Any(e => e.Id == model.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            model.Sessions = _context.Sessions
                .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name })
                .ToList();
            model.Trainees = _context.Users
                .Where(u => u.Role == UserRole.Trainee)
                .Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.FirstName })
                .ToList();

            return View(model);
        }

        // ========================
        // Delete GET
        // ========================
        public async Task<IActionResult> Delete(int id)
        {
            var grade = await _context.Grades
                .Include(g => g.Session)
                .Include(g => g.Trainee)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (grade == null) return NotFound();

            var model = new GradeViewModel
            {
                Id = grade.Id,
                Value = grade.Value,
                SessionName = grade.Session?.Name,
                TraineeName = grade.Trainee?.FirstName  
            };

            return View(model);
        }

        // ========================
        // Delete POST
        // ========================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grade = await _context.Grades.FindAsync(id);
            if (grade == null) return NotFound();

            _context.Grades.Remove(grade);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
