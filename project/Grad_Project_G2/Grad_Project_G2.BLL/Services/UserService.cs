using Grad_Project_G2.BLL.Services.Interfaces;
using Grad_Project_G2.BLL.ViewModels;
using Grad_Project_G2.BLL.ViewModels.UserViewModel;
using Grad_Project_G2.DAL.Models;
using Grad_Project_G2.DAL.Repositories.Interface;
using Grad_Project_G2.DAL.Repositories.UnitOfWork;

namespace Grad_Project_G2.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unit;

        public UserService(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public PagedResult<UserVM> GetAll(int page, int pageSize)
        {
            var users = _unit.Users.GetAllWithPagination(page, pageSize);
            var total = _unit.Users.GetTotalCount();

            return new PagedResult<UserVM>
            {
                Items = users.Select(u => new UserVM(u)),
                TotalItems = total,
                PageNumber = page,
                PageSize = pageSize
            };
        }

        public PagedResult<UserVM> Search(string? name, UserRole? role, int page, int pageSize)
        {
            var users = _unit.Users.Search(name, role, page, pageSize);
            var total = _unit.Users.GetSearchCount(name, role);

            return new PagedResult<UserVM>
            {
                Items = users.Select(u => new UserVM(u)),
                TotalItems = total,
                PageNumber = page,
                PageSize = pageSize
            };
        }

        public UserVM? GetById(int id)
        {
            var user = _unit.Users.GetById(id);
            return user == null ? null : new UserVM(user);
        }

        public void Create(CreateUserVM vm)
        {
            var user = new User
            {
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                Email = vm.Email,
                Role = vm.Role
            };
            _unit.Users.Add(user);
            _unit.Save();
        }

        public void Update(EditUserVM vm)
        {
            var user = _unit.Users.GetById(vm.Id);
            if (user == null) return;

            user.FirstName = vm.FirstName;
            user.LastName = vm.LastName;
            user.Email = vm.Email;
            user.Role = vm.Role;

            _unit.Users.Update(user);
            _unit.Save();
        }

        public void Delete(int id)
        {
            var user = _unit.Users.GetById(id);

            if (user == null) return;

            // Check if user is a trainee and has grades
            if (user.Role == UserRole.Trainee && user.Grades?.Any() == true)
            {
                //Delete all grade to this trainee
                foreach (var grade in user.Grades.ToList())
                {
                    _unit.Grades.Delete(grade.Id);
                }
            }
            _unit.Users.Delete(id);
            _unit.Save();
        }


        public bool EmailExists(string email, int? excludeId = null)
        {
            var user = _unit.Users.GetByEmail(email);
            return user != null && user.Id != excludeId;
        }
        public List<UserVM> GetAllTrainees()
        {
            return _unit.Users
                        .GetAll()
                        .Where(u => u.Role == UserRole.Trainee)
                        .Select(u => new UserVM(u))
                        .ToList();
        }
    }
}
