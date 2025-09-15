using Grad_Project_G2.BLL.Services;
using Grad_Project_G2.BLL.Services.Interfaces;
using Grad_Project_G2.BLL.ViewModels;
using Grad_Project_G2.DAL.Data;
using Grad_Project_G2.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Grad_Project_G2.Controllers
{
    public class GradeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ISessionService _sessionService;
        private readonly IUserService _userService;

        public GradeController(AppDbContext context, ISessionService sessionService, IUserService userService)
        {
            _context = context;
            _sessionService = sessionService;
            _userService = userService;
        }

        // ========================
        // Index
        // ========================
        public async Task<IActionResult> Index(string? search, int page = 1, int pageSize = 10)
        {
            var grades = await _context.Grades
                .Include(g => g.Session)
                .Include(g => g.Trainee)
                .ToListAsync();

            var gradeViewModels = grades
                .Where(g => string.IsNullOrEmpty(search) || g.Trainee.FirstName.Contains(search))
                .Select(g => new GradeViewModel
                {
                    Id = g.Id,
                    Value = g.Value,
                    SessionId = g.SessionId,
                    TraineeId = g.TraineeId,
                    SessionName = g.Session?.StartDate.ToString("yyyy-MM-dd HH:mm"),
                    TraineeName = g.Trainee?.FirstName + " " + g.Trainee?.LastName
                })
                .ToList();

            var pagedResult = new PagedResult<GradeViewModel>
            {
                Items = gradeViewModels.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                TotalItems = gradeViewModels.Count,
                PageNumber = page,
                PageSize = pageSize
            };

            ViewData["Search"] = search;
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
                .FirstOrDefaultAsync(g => g.Id == id);

            if (grade == null) return NotFound();

            var model = new GradeViewModel
            {
                Id = grade.Id,
                Value = grade.Value,
                SessionId = grade.SessionId,
                TraineeId = grade.TraineeId,
                SessionName = grade.Session?.StartDate.ToString("yyyy-MM-dd HH:mm"),
                TraineeName = grade.Trainee?.FirstName + " " + grade.Trainee?.LastName
            };

            return View(model);
        }

        // ========================
        // Create GET
        // ========================
        public IActionResult Create()
        {
            var model = new GradeViewModel();
            PopulateDropdowns(model);
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

            PopulateDropdowns(model);
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
                TraineeId = grade.TraineeId
            };

            PopulateDropdowns(model);
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
                var grade = await _context.Grades.FindAsync(id);
                if (grade == null) return NotFound();

                grade.Value = model.Value;
                grade.SessionId = model.SessionId;
                grade.TraineeId = model.TraineeId;

                _context.Update(grade);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            PopulateDropdowns(model);
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
                .FirstOrDefaultAsync(g => g.Id == id);

            if (grade == null) return NotFound();

            var model = new GradeViewModel
            {
                Id = grade.Id,
                Value = grade.Value,
                SessionName = grade.Session?.StartDate.ToString("yyyy-MM-dd HH:mm"),
                TraineeName = grade.Trainee?.FirstName + " " + grade.Trainee?.LastName
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

        // ========================
        // Helper: Populate Dropdowns
        // ========================
        private void PopulateDropdowns(GradeViewModel vm)
        {
            vm.Sessions = _sessionService.GetAllSessions(null, 1, 100)
                .Items
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.StartDate.ToString("yyyy-MM-dd HH:mm")
                })
                .ToList();

            vm.Trainees = _userService.GetAllTrainees()
                .Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.FullName
                })
                .ToList();
        }
    }
}
