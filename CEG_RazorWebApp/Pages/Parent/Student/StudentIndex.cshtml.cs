using AutoMapper;
using CEG_RazorWebApp.Libraries;
using CEG_RazorWebApp.Models.Student.Get;
using CEG_RazorWebApp.Pages.Admin.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace CEG_RazorWebApp.Pages.Parent.Children
{
    public class StudentIndexModel : PageModel
    {
        private readonly IConfiguration _config;

        public string? LayoutUrl { get; set; } = Constants.PARENT_LAYOUT_URL;

        public List<StudentListVM> StudentList { get; set; } = new List<StudentListVM>();

        public StudentIndexModel(IConfiguration config)
        {
            _config = config;
        }
        public void OnGet()
        {
            
        }
    }
}
