namespace CEG_RazorWebApp.Models.Admin.Response
{
    public class AdminAnswerCreateResponseVM
    {
        public AdminAnswerCreateResponseVM(bool status, string? errorMessage, string? successMessage)
        {
            Status = status;
            ErrorMessage = errorMessage;
            SuccessMessage = successMessage;
        }
        public AdminAnswerCreateResponseVM()
        {
            Status = false;
        }
        public bool Data { get; set; }
        public bool Status { get; set; }
        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }
    }
}
