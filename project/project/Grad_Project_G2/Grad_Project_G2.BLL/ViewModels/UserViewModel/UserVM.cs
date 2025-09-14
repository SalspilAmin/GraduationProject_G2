using Grad_Project_G2.DAL.Models;

namespace Grad_Project_G2.BLL.ViewModels.UserViewModel
{
    public class UserVM : BaseUserVM
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;

        public UserVM(User user)
        {
            Id = user.Id;
            FullName = $"{user.FirstName} {user.LastName}";
            Email = user.Email;
            Role = user.Role;
        }
    }
}
