using AutoMapper;
using CEG_RazorWebApp.Libraries;
using CEG_RazorWebApp.Models.Admin.Response;
using CEG_RazorWebApp.Models.Class.Create;
using CEG_RazorWebApp.Models.Class.Get;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace CEG_RazorWebApp.Pages.Admin.Class
{
    public class ClassIndexModel : PageModel
    {
        private readonly ChildrenEnglishGameLibrary methcall = new();
        [BindProperty]
        public List<IndexClassInfoVM>? Classes { get; set; } 
        [BindProperty]
        public CreateClassVM? CreateClass { get; set; } = new CreateClassVM();
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
