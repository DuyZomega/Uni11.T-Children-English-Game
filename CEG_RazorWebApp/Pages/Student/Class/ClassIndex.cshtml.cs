using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace CEG_RazorWebApp.Pages.Student.Class
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
