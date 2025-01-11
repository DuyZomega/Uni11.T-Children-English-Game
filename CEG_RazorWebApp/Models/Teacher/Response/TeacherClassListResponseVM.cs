using CEG_BAL.ViewModels;

namespace CEG_RazorWebApp.Models.Teacher.Response
{
    public class TeacherClassListResponseVM
    {
        public TeacherClassListResponseVM(bool status, string? errorMessage, string? successMessage)
        {
            Status = status;
            ErrorMessage = errorMessage;
            SuccessMessage = successMessage;
        }
        public TeacherClassListResponseVM()
        {
            Status = false;
        }
        public List<ClassViewModel>? Data { get; set; }
        public bool Status { get; set; }
        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }
    }
}
