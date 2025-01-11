using CEG_RazorWebApp.Libraries;
using CEG_RazorWebApp.Models.Class.Update;
using CEG_RazorWebApp.Models.Schedule.Create;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CEG_RazorWebApp.Pages.Admin.Class
{
    public class ClassInfoModel : PageModel
    {
        public int ClassID;
        public UpdateClassVM UpdateClassVM { get; set; } = new UpdateClassVM();
        public CreateScheduleVM CreateScheduleVM { get; set; } = new CreateScheduleVM();
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
