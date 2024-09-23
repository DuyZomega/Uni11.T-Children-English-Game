namespace CEG_WebMVC.Models.ViewModels.Admin.Response
{
    public class AdminSessionCreateResponseVM
    {
        public AdminSessionCreateResponseVM(bool status, string? errorMessage, string? successMessage)
        {
            Status = status;
            ErrorMessage = errorMessage;
            SuccessMessage = successMessage;
        }
        public AdminSessionCreateResponseVM()
        {
            Status = false;
        }
        public bool Data { get; set; }
        public bool Status { get; set; }
        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }
    }
}
