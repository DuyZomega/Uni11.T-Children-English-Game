using CEG_BAL.ViewModels;

namespace CEG_RazorWebApp.Models.Teacher.Response
{
    public class TeacherAccountListResponseVM
    {
        public TeacherAccountListResponseVM(bool status, string? errorMessage, string? successMessage)
        {
            Status = status;
            ErrorMessage = errorMessage;
            SuccessMessage = successMessage;
        }
        public TeacherAccountListResponseVM()
        {
            Status = false;
        }
        public List<AccountViewModel>? Data { get; set; }
        public bool Status { get; set; }
        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }
    }
}
