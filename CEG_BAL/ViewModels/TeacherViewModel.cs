namespace CEG_BAL.ViewModels
{
    public class TeacherViewModel
    {
        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string Address { get; set; } = null!;

        public virtual AccountViewModel Account { get; set; } = null!;

        public virtual ICollection<ClassViewModel> Classes { get; set; } = new List<ClassViewModel>();

        public virtual ICollection<CourseViewModel> Courses { get; set; } = new List<CourseViewModel>();
    }
}
