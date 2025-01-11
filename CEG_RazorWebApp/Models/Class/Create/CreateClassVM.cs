using CEG_BAL.Attributes;
using CEG_BAL.Configurations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CEG_RazorWebApp.Models.Class.Create
{
    public class CreateClassVM
    {
        [Required(ErrorMessage = "Class name is required")]
        [DisplayName("Class Name")]
        public string ClassName { get; set; } = null!;
        [Required(ErrorMessage = "Minimum students amount is required")]
        [Range(CEGConstants.CLASS_MINIMUM_STUDENTS_REQ, int.MaxValue)]
        [DisplayName("Minimum students amount")]
        public int MinimumStudents { get; set; } = CEGConstants.CLASS_MINIMUM_STUDENTS_REQ;
        [Required(ErrorMessage = "Maximum students amount is required")]
        [Range(CEGConstants.CLASS_MAXIMUM_STUDENTS_REQ, int.MaxValue)]
        [DisplayName("Maximum students amount")]
        public int MaximumStudents { get; set; } = CEGConstants.CLASS_MAXIMUM_STUDENTS_REQ;
        [Required(ErrorMessage = "Assign teacher name is required")]
        [DisplayName("Assign teacher name")]
        public string TeacherName { get; set; } = null!;
        [Required(ErrorMessage = "Assign course name is required")]
        [DisplayName("Assign course name")]
        public string CourseName { get; set; } = null!;
        [Required(ErrorMessage = "Class start date is required")]
        [DisplayName("Class start date")]
        [DataType(DataType.DateTime)]
        // [Range(typeof(DateTime), minimum: DateTime.Now.ToString(), "2025-12-31", ErrorMessage = "Start date must be between {1} and {2}.")]
        //startDate (30/9), endDate(30/10), daysInWeek(T2, T5) Phải sync ngày và thứ tạo (30/9 là T2)
        public DateTime StartDate { get; set; } = DateTime.Now.AddDays(10);
        [Required(ErrorMessage = "Class end date is required")]
        [DateGreaterThan("StartDate",CEGConstants.CLASS_MINIMUM_DAYS_REQ)]
        [DisplayName("Class end date")]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(40);
        [Required(ErrorMessage = "Enrollment Fee is required")]
        [Range(CEGConstants.CLASS_MINIMUM_ENROLLMENT_FEE, CEGConstants.CLASS_MAXIMUM_ENROLLMENT_FEE)]
        [DisplayName("Enrollment Fee")]
        public int EnrollmentFee { get; set; } = 1000000;
    }
}
