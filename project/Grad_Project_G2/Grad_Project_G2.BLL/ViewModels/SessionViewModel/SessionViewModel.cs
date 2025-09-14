using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;

namespace Grad_Project_G2.BLL.ViewModels
{
    public class SessionViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Course is required")]
        public int CourseId { get; set; }
        public string? CourseName { get; set; }

        [Required(ErrorMessage = "Start Date is required")]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "End Date is required")]
        [DataType(DataType.DateTime)]
        [CompareDate("StartDate", ErrorMessage = "End date must be after start date")]
        public DateTime EndDate { get; set; } = DateTime.Now;
        public List<SelectListItem>? Courses { get; set; }

    }

    // Custom Validation Attribute
    public class CompareDate : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public CompareDate(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var currentValue = (DateTime)value;
            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);
            if (property == null) return new ValidationResult($"Unknown property: {_comparisonProperty}");

            var comparisonValue = (DateTime)property.GetValue(validationContext.ObjectInstance);

            if (currentValue <= comparisonValue)
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
