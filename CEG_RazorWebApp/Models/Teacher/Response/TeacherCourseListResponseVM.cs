using CEG_BAL.ViewModels;

namespace CEG_RazorWebApp.Models.Teacher.Response
{
    public class TeacherCourseListResponseVM
    {
        public TeacherCourseListResponseVM(bool status, string? errorMessage, string? successMessage)
        {
            Status = status;
            ErrorMessage = errorMessage;
            SuccessMessage = successMessage;
        }
        public TeacherCourseListResponseVM()
        {
            Status = false;
        }
        public List<CourseViewModel>? Data { get; set; }
        public bool Status { get; set; }
        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }
    }
}
