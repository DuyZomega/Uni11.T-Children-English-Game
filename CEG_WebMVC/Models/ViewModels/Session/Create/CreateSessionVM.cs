using CEG_WebMVC.Libraries;
using CEG_WebMVC.Models.ViewModels.Homework.Create;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CEG_WebMVC.Models.ViewModels.Session.Create
{
    public class CreateSessionVM
    {
        public CreateSessionVM(int? courseId = null, string? courseName = null)
        {
            CourseId = courseId;
            CourseName = courseName;
            Hours = 1;
            Homeworks = new List<CreateHomeworkVM> { new CreateHomeworkVM() };
        }
        public CreateSessionVM()
        {
            Hours = 1;
            Homeworks = new List<CreateHomeworkVM> { new CreateHomeworkVM() };
        }
        [Required(ErrorMessage = "Session Title is required")]
        [DisplayName("Session Title")]
        public string Title { get; set; } = null!;
        [Required(ErrorMessage = "Description is required")]
        [DisplayName("Description")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Hours is required")]
        [Range(1, int.MaxValue)]
        [DisplayName("Hours")]
        public int? Hours { get; set; }
        public int? CourseId { get; set; }
        public string? CourseName { get; set; }
        public List<CreateHomeworkVM> Homeworks { get; set; } = new List<CreateHomeworkVM>();
    }
}
