using Grad_Project_G2.BLL.ViewModels;
using Grad_Project_G2.BLL.ViewModels.UserViewModel;
using Grad_Project_G2.DAL.Models;

namespace Grad_Project_G2.BLL.Services.Interfaces
{
    public interface IUserService
    {
        PagedResult<UserVM> GetAll(int page, int pageSize);
        PagedResult<UserVM> Search(string? name, UserRole? role, int page, int pageSize);
        UserVM? GetById(int id);
        void Create(CreateUserVM vm);
        void Update(EditUserVM vm);
        void Delete(int id);
        bool EmailExists(string email, int? excludeId = null);
        List<UserVM> GetAllTrainees();

    }
}
