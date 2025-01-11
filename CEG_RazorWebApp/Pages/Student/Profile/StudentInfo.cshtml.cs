using CEG_RazorWebApp.Libraries;
using CEG_RazorWebApp.Models.Account.Get;
using CEG_RazorWebApp.Models.Account.Update;
using CEG_RazorWebApp.Models.Student.Update;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CEG_RazorWebApp.Pages.Student.Profile
{
    public class StudentInfoModel : PageModel
    {
        public string? LayoutUrl { get; set; } = Constants.STUDENT_LAYOUT_URL;
        public AccountInfoVM? AccountInfo { get; set; } = new AccountInfoVM();
        public UpdateStudentVM? UpdateStudentInfo { get; set; } = new UpdateStudentVM();
        public UpdatePasswordVM? UpdatePasswordInfo { get; set; } = new UpdatePasswordVM();
        public void OnGet()
        {
        }
    }
}
