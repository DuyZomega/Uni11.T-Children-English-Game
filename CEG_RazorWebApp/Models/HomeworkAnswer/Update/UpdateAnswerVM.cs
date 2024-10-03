using CEG_RazorWebApp.Libraries;
using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CEG_RazorWebApp.Models.HomeworkAnswer.Update
{
    public class UpdateAnswerVM
    {
        public UpdateAnswerVM()
        {
            var lib = new ChildrenEnglishGameLibrary();
            Answer = "";
            Type = Constants.HOMEWORK_ANSWER_TYPE_CORRECT;
            DefaultAnswerTypeSelectList = lib.GetAnswerTypeSelectableList(Type);
        }
        public int? HomeworkAnswerId { get; set; }
        public string? Answer { get; set; }
        public string? Type { get; set; }
        public List<SelectListItem> DefaultAnswerTypeSelectList { get; set; }
    }
}
