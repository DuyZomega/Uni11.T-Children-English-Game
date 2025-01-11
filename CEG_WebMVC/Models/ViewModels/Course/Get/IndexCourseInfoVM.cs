using CEG_DAL.Models;

namespace CEG_WebMVC.Models.ViewModels.Course.Get
{
    public class IndexCourseInfoVM
    {
        public int? CourseId { get; set; }
        public string? CourseName { get; set; }

        public string? CourseType { get; set; }
        public int? TotalHours { get; set; }
        public string? CourseImageHeader { get; set; }
        public int? RequiredAge { get; set; }
        public string? Difficulty { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public int? SessionsAmount { get; set; }
        public int? ClassesAmount { get; set; }

        public string? Status { get; set; }
    }
}
