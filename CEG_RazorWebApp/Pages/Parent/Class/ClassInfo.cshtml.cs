using CEG_RazorWebApp.Libraries;
using CEG_RazorWebApp.Models.Enroll.Create;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CEG_RazorWebApp.Pages.Parent.Class
{
    public class ClassInfoModel : PageModel
    {
        private readonly CEG_RAZOR_Library methcall = new();

        public int? ClassID;

        public CreateEnrollVM CreateEnrollInfo { get; set; } = new CreateEnrollVM();
        public void OnGet(
            [FromRoute][Required] int classId)
        {
            ClassID = classId;
        }
    }
}
