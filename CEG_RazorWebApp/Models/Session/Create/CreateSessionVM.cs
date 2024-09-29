using CEG_RazorWebApp.Libraries;
using CEG_RazorWebApp.Models.Homework.Create;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CEG_RazorWebApp.Models.Session.Create
{
    public class CreateSessionVM
    {
        public CreateSessionVM(string? courseName = null)
        {
            Number = 1;
            CourseName = courseName;
            Hours = 1;
        }
        public CreateSessionVM()
        {
            Number = 1;
            Hours = 1;
        }
        [Required(ErrorMessage = "Session Number is required")]
        [Range(1, int.MaxValue)]
        [DisplayName("Number")]
        public int Number { get; set; }
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
        public string? CourseName { get; set; }
    }
}
