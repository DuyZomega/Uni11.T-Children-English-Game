using CEG_BAL.ViewModels;

namespace CEG_WebMVC.Models.ViewModels.Admin.Response
{
    public class AdminClassListResponseVM
    {
        public AdminClassListResponseVM(bool status, string? errorMessage, string? successMessage)
        {
            Status = status;
            ErrorMessage = errorMessage;
            SuccessMessage = successMessage;
        }
        public AdminClassListResponseVM()
        {
            Status = false;
        }
        public List<ClassViewModel>? Data { get; set; }
        public bool Status { get; set; }
        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }
    }
}
