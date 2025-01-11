using CEG_BAL.ViewModels;

namespace CEG_WebMVC.Models.ViewModels.Admin.Response
{
    public class AdminAccountListResponseVM
    {
        public AdminAccountListResponseVM(bool status, string? errorMessage, string? successMessage)
        {
            Status = status;
            ErrorMessage = errorMessage;
            SuccessMessage = successMessage;
        }
        public AdminAccountListResponseVM()
        {
            Status = false;
        }
        public List<AccountViewModel>? Data { get; set; }
        public bool Status { get; set; }
        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }
    }
}
