using CEG_BAL.ViewModels;

namespace CEG_RazorWebApp.Models.Parent.Response
{
    public class ParentCourseListResponseVM
    {
        public ParentCourseListResponseVM(bool status, string? errorMessage, string? successMessage)
        {
            Status = status;
            ErrorMessage = errorMessage;
            SuccessMessage = successMessage;
        }
        public ParentCourseListResponseVM()
        {
            Status = false;
        }
        public List<CourseViewModel>? Data { get; set; }
        public bool Status { get; set; }
        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }
    }
}
