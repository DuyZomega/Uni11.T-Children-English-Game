using CEG_RazorWebApp.Libraries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CEG_RazorWebApp.Pages.Admin.Class
{
    public class ClassInfoModel : PageModel
    {
        private readonly CEG_RAZOR_Library methcall = new();
        public int? ClassID;
        public void OnGet(
            [FromRoute][Required] int classId)
        {
            methcall.InitTempData(this);
            ClassID = classId;
        }
        /*public IActionResult OnGetLogout()
        {
            //_httpClient.DefaultRequestHeaders.Authorization = null;
            HttpContext.Session.Clear();
            TempData.Clear();
            SignOut();

            // If using ASP.NET Identity, you may want to sign out the user
            // Example: await SignInManager.SignOutAsync();

            return RedirectToPage("/Home/Index");
        }*/
    }
}
