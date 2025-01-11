using AutoMapper;
using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Admin;
using CEG_RazorWebApp.Libraries;
using CEG_RazorWebApp.Models.Admin.Response;
using CEG_RazorWebApp.Models.Homework.Create;
using CEG_RazorWebApp.Models.Homework.Get;
using CEG_RazorWebApp.Models.Homework.Update;
using CEG_RazorWebApp.Models.Session.Get;
using CEG_RazorWebApp.Models.Session.Update;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace CEG_RazorWebApp.Pages.Admin.Course
{
    public class SessionInfoModel : PageModel
    {
        public string? LayoutUrl { get; set; } = Constants.ADMIN_LAYOUT_URL;
        [BindProperty]
        public int? CourseID { get; set; }
        public int? SessionID { get; set; }
        // public SessionInfoVM? SessionInfo { get; set; }
        public UpdateSessionVM? UpdateSessionInfo { get; set; } = new UpdateSessionVM();
        public CreateHomeworkVM? CreateHomework { get; set; } = new CreateHomeworkVM();
        public void OnGet(
            [FromRoute][Required] int courseId,
            [FromRoute][Required] int sessionId)
        {
            CourseID = courseId;
            SessionID = sessionId;
        }
    }
}
