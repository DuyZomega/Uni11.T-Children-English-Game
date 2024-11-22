using CEG_RazorWebApp.Libraries;
using CEG_RazorWebApp.Models.Class.Update;
using CEG_RazorWebApp.Models.Schedule.Create;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CEG_RazorWebApp.Pages.Student.Class
{
    public class ClassInfoModel : PageModel
    {
        private readonly CEG_RAZOR_Library methcall = new();
        public int? ClassID;
        public UpdateClassVM UpdateClassVM { get; set; } = new UpdateClassVM();
        public void OnGet(
            [FromRoute][Required] int classId)
        {
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
