using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CEG_RazorWebApp.Pages.Parent.Class
{
    public class ClassIndexModel : PageModel
    {
        [BindProperty]
        public string? SchedulePresets { get; set; }
        public void OnGet()
        {
        }
    }
}
