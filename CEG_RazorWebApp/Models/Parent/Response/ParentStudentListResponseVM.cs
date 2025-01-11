using CEG_BAL.ViewModels;

namespace CEG_RazorWebApp.Models.Parent.Response
{
    public class ParentStudentListResponseVM
    {
        public ParentStudentListResponseVM(bool status, string? errorMessage, string? successMessage)
        {
            Status = status;
            ErrorMessage = errorMessage;
            SuccessMessage = successMessage;
        }
        public ParentStudentListResponseVM()
        {
            Status = false;
        }
        public List<StudentViewModel>? Data { get; set; }
        public bool Status { get; set; }
        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }
    }
}
