using CEG_BAL.ViewModels;

namespace CEG_RazorWebApp.Models.Teacher.Response
{
    public class TeacherSessionInfoResponseVM
    {
        public TeacherSessionInfoResponseVM(bool status, string? errorMessage, string? successMessage)
        {
            Status = status;
            ErrorMessage = errorMessage;
            SuccessMessage = successMessage;
        }
        public TeacherSessionInfoResponseVM()
        {
            Status = false;
        }
        public SessionViewModel? Data { get; set; }
        public bool Status { get; set; }
        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }
    }
}
