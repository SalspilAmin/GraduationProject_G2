using Grad_Project_G2.DAL.Data;
using Grad_Project_G2.DAL.Models;
using Grad_Project_G2.DAL.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Grad_Project_G2.DAL.Repositories
{
    public class UserRepository : GenericRepositories<User>, IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public User? GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }


        public IEnumerable<User> Search(string? searchTerm, UserRole? role, int page, int pageSize)
        {
            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(u =>
                    u.FirstName.Contains(searchTerm) ||
                    u.LastName.Contains(searchTerm) ||
                    u.Email.Contains(searchTerm));
            }

            if (role.HasValue)
                query = query.Where(u => u.Role == role);

            return query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public int GetSearchCount(string? searchTerm, UserRole? role)
        {
            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(u =>
                    u.FirstName.Contains(searchTerm) ||
                    u.LastName.Contains(searchTerm) ||
                    u.Email.Contains(searchTerm));
            }

            if (role.HasValue)
                query = query.Where(u => u.Role == role);

            return query.Count();
        }

    }
}
