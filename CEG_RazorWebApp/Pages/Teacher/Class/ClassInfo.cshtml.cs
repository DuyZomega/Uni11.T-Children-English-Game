using AutoMapper;
using CEG_RazorWebApp.Libraries;
using CEG_RazorWebApp.Models.Class.Get;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace CEG_RazorWebApp.Pages.Teacher.Class
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
