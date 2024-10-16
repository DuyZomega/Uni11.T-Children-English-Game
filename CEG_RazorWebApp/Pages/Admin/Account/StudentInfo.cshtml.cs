using CEG_RazorWebApp.Libraries;
using CEG_RazorWebApp.Models.Account.Get;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CEG_RazorWebApp.Pages.Admin.Account
{
    public class StudentInfoModel : PageModel
    {
        private CEG_RAZOR_Library methcall = new();
        public string? LayoutUrl { get; set; } = Constants.ADMIN_LAYOUT_URL;
        public AccountInfoVM? AccountInfo { get; set; } = new AccountInfoVM();
        public int AccountId = 0;
        public void OnGet(
            [FromRoute][Required] int accountId)
        {
            methcall.InitTempData(this);
            AccountId = accountId;
        }
        public IActionResult OnGetLogout()
        {
            //_httpClient.DefaultRequestHeaders.Authorization = null;
            HttpContext.Session.Clear();
            TempData.Clear();
            SignOut();

            // If using ASP.NET Identity, you may want to sign out the user
            // Example: await SignInManager.SignOutAsync();

            return RedirectToPage("/Home/Index");
        }
    }
}
