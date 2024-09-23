using CEG_BAL.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using CEG_DAL.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CEG_WebMVC.Models.ViewModels.Homework.Create
{
    public class CreateHomeworkVM
    {
        public CreateHomeworkVM(int? courseId = null, string? sessionTitle = null)
        {
            CourseId = courseId;
            SessionTitle = sessionTitle;
            Hours = 1;
        }
        public CreateHomeworkVM()
        {
            Hours = 1;
        }
        [Required(ErrorMessage = "Hours is required")]
        [Range(1, int.MaxValue)]
        [DisplayName("Hours")]
        public int? Hours { get; set; }
        public int? CourseId { get; set; }
        public string? SessionTitle { get; set; }
    }
}
