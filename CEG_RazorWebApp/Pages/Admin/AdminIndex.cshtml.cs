using AutoMapper;
using CEG_RazorWebApp.Libraries;
using CEG_RazorWebApp.Pages.Admin.Course;
using CEG_RazorWebApp.Pages.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

namespace CEG_RazorWebApp.Pages.Admin
{
    //[Authorize(Policy = "SessionAuthorize")]
    public class AdminIndexModel : PageModel
    {
        public string? LayoutUrl { get; set; } = Constants.ADMIN_LAYOUT_URL;

        public void OnGet()
        {

        }
    }
}
