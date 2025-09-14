using Grad_Project_G2.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Grad_Project_G2.BLL.ViewModels.UserViewModel
{
    public class BaseUserVM
    {
        [Required, StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required, EmailAddress]
        [Remote(action: "IsEmailUnique", controller: "User", AdditionalFields = "Id", ErrorMessage = "Email already exists")]
        public string Email { get; set; } = string.Empty;

        [Required]
        public UserRole Role { get; set; }
    }
}
