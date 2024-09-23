namespace CEG_WebMVC.Models.ViewModels.Admin.Response
{
    public class AdminCourseCreateResponseVM
    {
        public AdminCourseCreateResponseVM(bool status, string? errorMessage, string? successMessage)
        {
            Status = status;
            ErrorMessage = errorMessage;
            SuccessMessage = successMessage;
        }
        public AdminCourseCreateResponseVM()
        {
            Status = false;
        }
        public bool Data { get; set; }
        public bool Status { get; set; }
        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }
    }
}
