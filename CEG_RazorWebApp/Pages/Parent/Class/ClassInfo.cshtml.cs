using CEG_RazorWebApp.Libraries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CEG_RazorWebApp.Pages.Parent.Class
{
    public class ClassInfoModel : PageModel
    {
        public int? ClassID { get; set; }
        public void OnGet(
            [FromRoute][Required] int classId)
        {
            ClassID = classId;
        }
    }
}
