using CEG_RazorWebApp.Libraries;
using CEG_RazorWebApp.Models.HomeworkQuestion.Create;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CEG_RazorWebApp.Pages.Admin.Question
{
    public class QuestionIndexModel : PageModel
    {
        public CreateQuestionVM? CreateQuestion { get; set; } = new CreateQuestionVM();
        private CEG_RAZOR_Library methcall = new();
        public string? LayoutUrl { get; set; } = Constants.ADMIN_LAYOUT_URL;
        public void OnGet()
        {
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
