using CEG_BAL.ViewModels;

namespace CEG_WebMVC.Models.ViewModels.Course.Response
{
    public class AdminCourseInfoResponseVM
    {
        public AdminCourseInfoResponseVM(bool status, string? errorMessage, string? successMessage)
        {
            Status = status;
            ErrorMessage = errorMessage;
            SuccessMessage = successMessage;
        }
        public AdminCourseInfoResponseVM()
        {
            Status = false;
        }
        public CourseViewModel? Data { get; set; }
        public bool Status { get; set; }
        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }
    }
}
