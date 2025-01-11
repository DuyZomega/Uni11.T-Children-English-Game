using CEG_BAL.ViewModels;

namespace CEG_WebMVC.Models.ViewModels.Course.Get
{
    public class CourseInfoVM
    {
        public CourseInfoVM()
        {
            SessionsAmount = 0;
            ClassesAmount = 0;
        }
        
        public int? CourseId { get; set; }
        public string CourseName { get; set; } = null!;

        public string CourseType { get; set; } = null!;

        public string Description { get; set; } = null!;
        public int? TotalHours { get; set; }
        public string? CourseImageHeader { get; set; }
        public int? RequiredAge { get; set; }
        public string? Difficulty { get; set; }
        public string? Category { get; set; }
        public int? SessionsAmount { get; set; }
        public int? ClassesAmount { get; set; }

        public string? Status { get; set; }
    }
}
