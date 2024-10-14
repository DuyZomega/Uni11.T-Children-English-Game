using CEG_RazorWebApp.Libraries;
using CEG_RazorWebApp.Models.Homework.Get;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CEG_RazorWebApp.Pages.Student.Homework
{
    public class HomeworkIndexModel : PageModel
    {
        private readonly ChildrenEnglishGameLibrary methcall = new();
        [BindProperty]
        public List<HomeworkInfoVM>? Homeworks { get; set; }
        public void OnGet()
        {
            methcall.InitTempData(this);
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
