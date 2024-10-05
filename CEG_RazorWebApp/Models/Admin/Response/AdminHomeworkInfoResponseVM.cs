using CEG_BAL.ViewModels;

namespace CEG_RazorWebApp.Models.Admin.Response
{
    public class AdminHomeworkInfoResponseVM
    {
        public AdminHomeworkInfoResponseVM(bool status, string? errorMessage, string? successMessage)
        {
            Status = status;
            ErrorMessage = errorMessage;
            SuccessMessage = successMessage;
        }
        public AdminHomeworkInfoResponseVM()
        {
            Status = false;
        }
        public HomeworkViewModel? Data { get; set; }
        public bool Status { get; set; }
        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }
    }
}
