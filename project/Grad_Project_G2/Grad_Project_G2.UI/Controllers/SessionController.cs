using Grad_Project_G2.BLL.Services;
using Grad_Project_G2.BLL.ViewModels;
using Grad_Project_G2.DAL.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Grad_Project_G2.UI.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionService;
        private readonly IUnitOfWork _unitOfWork;


        public SessionController(ISessionService sessionService , IUnitOfWork unitOfWork)
        {
            _sessionService = sessionService;
            _unitOfWork = unitOfWork;
        }

        // GET: Session/Index
        public IActionResult Index(string? search, int page = 1, int pageSize = 5)
        {
            var sessions = _sessionService.GetAllSessions(search, page, pageSize);
            ViewBag.SearchTerm = search;


            return View(sessions);
        }

        // GET: Session/Details/5
        public IActionResult Details(int id)
        {
            var session = _sessionService.GetSessionById(id);
            if (session == null)
                return NotFound();

            return View(session);
        }

        // GET: Session/Create
        public IActionResult Create()
        {
            ViewBag.Courses = _unitOfWork.Courses.GetAll()
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
                .ToList();

            return View(new SessionViewModel());
        }

        // POST: Session/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SessionViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _sessionService.AddSession(vm);
                TempData["Success"] = "Session created successfully!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Courses = _unitOfWork.Courses.GetAll()
            .Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                 Text = c.Name
             })
              .ToList();
            return View(vm);
        }

        // GET: Session/Edit/5
        public IActionResult Edit(int id)
        {
            var session = _sessionService.GetSessionById(id);
            if (session == null)
                return NotFound();

            ViewBag.Courses = _unitOfWork.Courses.GetAll()
                .Select(c => new SelectListItem
                    {
                       Value = c.Id.ToString(),
                       Text = c.Name,
                       Selected = (c.Id == session.CourseId) 
                    })
                      .ToList();



            return View(session);
        }

        // POST: Session/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SessionViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _sessionService.UpdateSession(vm);
                TempData["Success"] = "Session updated successfully!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Courses = _unitOfWork.Courses.GetAll()
                            .Select(c => new SelectListItem
                                {
                                   Value = c.Id.ToString(),
                                  Text = c.Name,
                                  Selected = (c.Id == vm.CourseId)
                                })
                             .ToList();



            return View(vm);
        }

        // GET: Session/Delete/5
        public IActionResult Delete(int id)
        {
            var session = _sessionService.GetSessionById(id);
            if (session == null)
                return NotFound();

            return View(session);
        }

        // POST: Session/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _sessionService.DeleteSession(id);
            TempData["Success"] = "Session deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
