using Grad_Project_G2.DAL.Models;

namespace Grad_Project_G2.DAL.Repositories.Interface
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User? GetByEmail(string email);
        IEnumerable<User> Search(string? name, UserRole? role, int page, int pageSize);
        int GetSearchCount(string? name, UserRole? role);
    }
}
