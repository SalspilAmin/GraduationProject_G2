using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grad_Project_G2.BLL.Validations
{
    internal class NoNumAttribute:ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null) return true;
            var strValue = value.ToString();
            ErrorMessage = "The name shouldn't contain digits";
            return !(strValue?.Any(char.IsDigit)==true);
        }
    }
}
