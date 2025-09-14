using Grad_Project_G2.DAL.Models;

namespace Grad_Project_G2.BLL.ViewModels.UserViewModel
{
    public class DetailsUserVM : BaseUserVM
    {
        public int Id { get; set; }
        public string FullName => $"{FirstName} {LastName}";
    }
}
