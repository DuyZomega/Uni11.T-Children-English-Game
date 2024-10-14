using CEG_RazorWebApp.Libraries;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CEG_RazorWebApp.Models.HomeworkAnswer.Create
{
    public class CreateAnswerVM
    {
        public CreateAnswerVM()
        {
            var lib = new CEG_RAZOR_Library();
            Answer = "";
            Type = Constants.HOMEWORK_ANSWER_TYPE_CORRECT;
            DefaultAnswerTypeSelectList = lib.GetAnswerTypeSelectableList(Type);
        }
        [Required(ErrorMessage = "Answer Description is required")]
        public string? Answer { get; set; }
        [Required(ErrorMessage = "Answer Type is required")]
        public string? Type { get; set; }
        public List<SelectListItem> DefaultAnswerTypeSelectList { get; set; }
        public int? QuestionId { get; set; }
        //public string? Question { get; set; }
    }
}
