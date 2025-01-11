using CEG_BAL.ViewModels;

namespace CEG_RazorWebApp.Models.Teacher.Response
{
    public class TeacherHomeworkInfoResponseVM
    {
        public TeacherHomeworkInfoResponseVM(bool status, string? errorMessage, string? successMessage)
        {
            Status = status;
            ErrorMessage = errorMessage;
            SuccessMessage = successMessage;
        }
        public TeacherHomeworkInfoResponseVM()
        {
            Status = false;
        }
        public HomeworkViewModel? Data { get; set; }
        public bool Status { get; set; }
        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }
    }
}
