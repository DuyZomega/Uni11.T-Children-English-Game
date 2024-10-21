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
        [BindProperty]
        public CreateClassVM? CreateClass { get; set; } = new CreateClassVM();
        public string? SchedulePresets { get; set; }
        public void OnGet()
        {
            SchedulePresets = JsonSerializer.Serialize(CreateClass.WeeklySchedulePresets);
        }
    }
}
