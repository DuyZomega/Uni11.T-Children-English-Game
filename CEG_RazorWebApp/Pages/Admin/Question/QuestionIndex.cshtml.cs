using CEG_RazorWebApp.Libraries;
using CEG_RazorWebApp.Models.HomeworkQuestion.Create;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CEG_RazorWebApp.Pages.Admin.Question
{
    public class QuestionIndexModel : PageModel
    {
        public CreateQuestionVM? CreateQuestion { get; set; } = new CreateQuestionVM();
        private ChildrenEnglishGameLibrary methcall = new();
        public string? LayoutUrl { get; set; } = Constants.ADMIN_LAYOUT_URL;
        public void OnGet()
        {
        }
    }
}
