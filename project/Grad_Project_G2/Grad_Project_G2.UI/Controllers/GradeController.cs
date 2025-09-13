using Microsoft.AspNetCore.Mvc;
using Grad_Project_G2.DAL.Models;
using Grad_Project_G2.BLL.Services;
using Grad_Project_G2.BLL.ViewModels;

public class GradeController : Controller
{
    private readonly GradeService _gradeService;

    public GradeController(GradeService gradeService)
    {
        _gradeService = gradeService;
    }

    // GET: Grades
    public async Task<IActionResult> Index(string? search, int pageIndex = 1)
    {
        int pageSize = 10;

        var grades = await _gradeService.GetGradesAsync(search, pageIndex, pageSize);

        ViewData["Search"] = search;

        return View(grades);
    }

    // GET: Grades/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var grade = await _gradeService.GetGradeByIdAsync(id);
        if (grade == null)
        {
            return NotFound();
        }
        return View(grade);
    }

    // GET: Grades/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Grades/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Grade grade)
    {
        if (ModelState.IsValid)
        {
            await _gradeService.AddGradeAsync(grade);
            return RedirectToAction(nameof(Index));
        }
        return View(grade);
    }

    // GET: Grades/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var grade = await _gradeService.GetGradeByIdAsync(id);
        if (grade == null)
        {
            return NotFound();
        }

        // Convert ViewModel back to Entity if needed
        var gradeEntity = new Grade
        {
            Id = grade.Id,
            Value = grade.Value,
        };

        return View(gradeEntity);
    }

    // POST: Grades/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Grade grade)
    {
        if (id != grade.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await _gradeService.UpdateGradeAsync(grade);
            return RedirectToAction(nameof(Index));
        }
        return View(grade);
    }

    // GET: Grades/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var grade = await _gradeService.GetGradeByIdAsync(id);
        if (grade == null)
        {
            return NotFound();
        }

        return View(grade);
    }

    // POST: Grades/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _gradeService.DeleteGradeAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
