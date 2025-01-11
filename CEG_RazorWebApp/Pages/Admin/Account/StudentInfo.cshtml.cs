using CEG_RazorWebApp.Libraries;
using CEG_RazorWebApp.Models.Account.Get;
using CEG_RazorWebApp.Models.Account.Update;
using CEG_RazorWebApp.Models.Student.Update;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CEG_RazorWebApp.Pages.Admin.Account
{
    public class StudentInfoModel : PageModel
    {
        public string? LayoutUrl { get; set; } = Constants.ADMIN_LAYOUT_URL;
        public AccountInfoVM? AccountInfo { get; set; } = new AccountInfoVM();
        public UpdateStudentVM? UpdateStudentInfo { get; set; } = new UpdateStudentVM();
        public UpdatePasswordVM? UpdatePasswordInfo { get; set; } = new UpdatePasswordVM();
        public int AccountId = 0;
        public void OnGet(
            [FromRoute][Required] int accountId)
        {
            AccountId = accountId;
        }
    }
}
