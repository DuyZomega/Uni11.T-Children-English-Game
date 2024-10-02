using CEG_BAL.ViewModels;

namespace CEG_RazorWebApp.Models.Admin.Response
{
    public class AdminSessionInfoResponseVM
    {
        public AdminSessionInfoResponseVM(bool status, string? errorMessage, string? successMessage)
        {
            Status = status;
            ErrorMessage = errorMessage;
            SuccessMessage = successMessage;
        }
        public AdminSessionInfoResponseVM()
        {
            Status = false;
        }
        public SessionViewModel? Data { get; set; }
        public bool Status { get; set; }
        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }
    }
}
