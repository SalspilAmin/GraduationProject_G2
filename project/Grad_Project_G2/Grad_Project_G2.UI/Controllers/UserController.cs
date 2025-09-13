using Grad_Project_G2.BLL.Services.Interfaces;
using Grad_Project_G2.BLL.ViewModels;
using Grad_Project_G2.BLL.ViewModels.UserViewModel;
using Grad_Project_G2.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Grad_Project_G2.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: User
        public IActionResult Index(string? search, UserRole? role, int page = 1, int pageSize = 2)
        {
            var result = _userService.Search(search, role, page, pageSize);
            ViewBag.Search = search;
            ViewBag.Role = role;
            return View(result);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public IActionResult Create(CreateUserVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            _userService.Create(vm);
            TempData["Success"] = "User created successfully!";
            return RedirectToAction(nameof(Index));
        }

        // GET: User/Edit/5
        public IActionResult Edit(int id)
        {
            var user = _userService.GetById(id);
            if (user == null) return NotFound();

            var vm = new EditUserVM
            {
                Id = user.Id,
                FirstName = user.FullName.Split(' ')[0],
                LastName = user.FullName.Split(' ').Length > 1 ? user.FullName.Split(' ')[1] : "",
                Email = user.Email,
                Role = user.Role
            };

            return View(vm);
        }

        // POST: User/Edit
        [HttpPost]
        public IActionResult Edit(EditUserVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            _userService.Update(vm);
            TempData["Success"] = "User updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        // GET: User/Delete/5
        public IActionResult Delete(int id)
        {
            var user = _userService.GetById(id);
            if (user == null) return NotFound();

            var vm = new DeleteUserVM
            {
                Id = user.Id,
                FullName = user.FullName
            };

            return View(vm);
        }

        // POST: User/Delete
        [HttpPost, ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            _userService.Delete(id);
            TempData["Success"] = "User deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        // GET: User/Details/5
        public IActionResult Details(int id)
        {
            var user = _userService.GetById(id);
            if (user == null) return NotFound();

            var vm = new DetailsUserVM
            {
                Id = user.Id,
                FirstName = user.FullName.Split(' ')[0],
                LastName = user.FullName.Split(' ').Length > 1 ? user.FullName.Split(' ')[1] : "",
                Email = user.Email,
                Role = user.Role
            };

            return View(vm);
        }

        // Remote Validation
        [AcceptVerbs("GET", "POST")]
        public IActionResult IsEmailUnique(string email, int? id)
        {
            var exists = _userService.EmailExists(email, id);
            return Json(!exists);
        }
    }
}
