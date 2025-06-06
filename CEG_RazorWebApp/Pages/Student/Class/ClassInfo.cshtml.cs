using AutoMapper;
using CEG_DAL.Models;
using CEG_DAL.Repositories.Implements;
using CEG_RazorWebApp.Libraries;
using CEG_RazorWebApp.Models.Class.Get;
using CEG_RazorWebApp.Models.Class.Update;
using CEG_RazorWebApp.Models.Enroll.Create;
using CEG_RazorWebApp.Models.Schedule.Create;
using CEG_RazorWebApp.Models.Student.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace CEG_RazorWebApp.Pages.Student.Class
{
    public class ClassInfoModel : PageModel
    {
        public int ClassID { get; set; }
        public void OnGet(
            [FromRoute][Required] int classId)
        {
            ClassID = classId;
        }
    }
}
