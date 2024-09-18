using CEG_BAL.ViewModels;

namespace CEG_WebMVC.Models.ViewModels.Class.Get
{
    public class IndexClassInfoVM
    {
        public string ClassName { get; set; } = null!;

        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? MinimumStudents { get; set; }
        public int? MaximumStudents { get; set; }
        public int? TeacherName { get; set; }
        public int? CurrentStudentAmount { get; set; }


        /*public virtual ICollection<StudentProcessViewModel> StudentProcesses { get; set; } = new List<StudentProcessViewModel>();*/

        //public virtual TeacherViewModel Teacher { get; set; } = null!;
    }
}
