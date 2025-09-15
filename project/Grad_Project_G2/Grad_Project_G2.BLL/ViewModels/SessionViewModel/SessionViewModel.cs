using Microsoft.AspNetCore.Mvc;
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
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required")]
        [DataType(DataType.DateTime)]
        [Remote(action: "ValidateEndDate", controller: "Session", AdditionalFields = nameof(StartDate), ErrorMessage = "End date must be after start date.")]
        public DateTime EndDate { get; set; }


    }

}
