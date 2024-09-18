namespace CEG_WebMVC.Models.ViewModels.Class.Create
{
    public class CreateClassVM
    {
        public CreateClassVM()
        {

        }
        public string ClassName { get; set; }
        public int MinStudents { get; set; }
        public int MaxStudents { get; set; }
        public string TeacherName { get; set; }
        public string CourseName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
