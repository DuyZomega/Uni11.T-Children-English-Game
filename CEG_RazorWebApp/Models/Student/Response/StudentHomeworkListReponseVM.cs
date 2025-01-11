using CEG_BAL.ViewModels;

namespace CEG_RazorWebApp.Models.Student.Response
{
    public class StudentHomeworkListReponseVM
    {
        public StudentHomeworkListReponseVM(bool status, string? errorMessage, string? successMessage)
        {
            Status = status;
            ErrorMessage = errorMessage;
            SuccessMessage = successMessage;
        }
        public StudentHomeworkListReponseVM()
        {
            Status = false;
        }
        public List<HomeworkViewModel>? Data { get; set; }
        public bool Status { get; set; }
        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }
    }
}
