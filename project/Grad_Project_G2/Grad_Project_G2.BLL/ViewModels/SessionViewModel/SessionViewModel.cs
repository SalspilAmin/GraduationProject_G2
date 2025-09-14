using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required")]
        [DataType(DataType.DateTime)]
        [CompareDate("StartDate", ErrorMessage = "End date must be after start date")]
        public DateTime EndDate { get; set; }


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
            var currentValue = (DateTime?)value;

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);
            if (property == null)
                throw new ArgumentException("Property with this name not found");

            var comparisonValue = (DateTime?)property.GetValue(validationContext.ObjectInstance);

            if (currentValue.HasValue && comparisonValue.HasValue)
            {
                // Compare full DateTime (date + time)
                if (currentValue.Value <= comparisonValue.Value)
                {
                    return new ValidationResult(ErrorMessage ?? $"{validationContext.DisplayName} must be after {_comparisonProperty}");
                }
            }

            return ValidationResult.Success;
        }
    }
}
