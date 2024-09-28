using CEG_BAL.ViewModels.Authenticates;

namespace CEG_WebMVC.Models.Account.Response
{
    public class AuthenResponseVM
    {
        public AuthenResponseVM(bool status, string? errorMessage, string? successMessage)
        {
            Status = status;
            ErrorMessage = errorMessage;
            SuccessMessage = successMessage;
        }
        public AuthenResponseVM()
        {
            Status = false;
        }
        public AuthenResponse? Data { get; set; }
        public bool Status { get; set; }
        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }
    }
}
