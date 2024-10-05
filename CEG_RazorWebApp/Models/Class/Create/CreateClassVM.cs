using CEG_BAL.Attributes;
using CEG_RazorWebApp.Libraries;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CEG_RazorWebApp.Models.Class.Create
{
    public class CreateClassVM
    {
        [Required(ErrorMessage = "Class name is required")]
        [DisplayName("Class Name")]
        public string ClassName { get; set; }
        [Required(ErrorMessage = "Minimum students amount is required")]
        [Range(Constants.CLASS_MINIMUM_STUDENTS_REQ, int.MaxValue)]
        [DisplayName("Minimum students amount")]
        public int MinStudents { get; set; }
        [Required(ErrorMessage = "Maximum students amount is required")]
        [Range(Constants.CLASS_MAXIMUM_STUDENTS_REQ, int.MaxValue)]
        [DisplayName("Maximum students amount")]
        public int MaxStudents { get; set; }
        [Required(ErrorMessage = "Assign teacher name is required")]
        [DisplayName("Assign teacher name")]
        public string TeacherName { get; set; }
        [Required(ErrorMessage = "Assign course name is required")]
        [DisplayName("Assign course name")]
        public string CourseName { get; set; }
        [Required(ErrorMessage = "Class start date is required")]
        [DisplayName("Class start date")]
        [DataType(DataType.DateTime)]
        //startDate (30/9), endDate(30/10), daysInWeek(T2, T5) Phải sync ngày và thứ tạo (30/9 là T2)
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Course end date is required")]
        [DateGreaterThan("StartDate",Constants.CLASS_MINIMUM_DAYS_REQ)]
        [DisplayName("Class end date")]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }
    }
}
