using CEG_BAL.ViewModels;

namespace CEG_WebMVC.Models.ViewModels.Class.Get
{
    public class ClassInfoVM
    {
        public string ClassName { get; set; } = null!;

        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? MinimumStudents { get; set; }
        public int? MaximumStudents { get; set; }

        public virtual CourseViewModel Course { get; set; } = new CourseViewModel();

        public virtual ICollection<StudentProgressViewModel> StudentProcesses { get; set; } = new List<StudentProgressViewModel>();

        public virtual TeacherViewModel Teacher { get; set; } = null!;
    }
}
