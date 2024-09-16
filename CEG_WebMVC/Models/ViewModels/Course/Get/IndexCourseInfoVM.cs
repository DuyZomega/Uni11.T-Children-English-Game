using CEG_DAL.Models;

namespace CEG_WebMVC.Models.ViewModels.Course.Get
{
    public class IndexCourseInfoVM
    {
        public string CourseName { get; set; } = null!;

        public string CourseType { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string? Status { get; set; }
    }
}
