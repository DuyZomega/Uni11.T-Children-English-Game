using CEG_RazorWebApp.Libraries;
using CEG_RazorWebApp.Models.Account.Get;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CEG_RazorWebApp.Pages.Teacher.Profile
{
    public class TeacherInfoModel : PageModel
    {
        public string? LayoutUrl { get; set; } = Constants.TEACHER_LAYOUT_URL;
        public AccountInfoVM AccountInfo { get; set; } = new AccountInfoVM();
        public void OnGet()
        {
        }
    }
}
