using CEG_BAL.ViewModels;

namespace CEG_RazorWebApp.Models.Parent.Response
{
    public class ParentCourseInfoResponseVM
    {
        public ParentCourseInfoResponseVM(bool status, string? errorMessage, string? successMessage)
        {
            Status = status;
            ErrorMessage = errorMessage;
            SuccessMessage = successMessage;
        }
        public ParentCourseInfoResponseVM()
        {
            Status = false;
        }
        public CourseViewModel? Data { get; set; }
        public bool Status { get; set; }
        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }
    }
}
