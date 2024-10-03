using CEG_BAL.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using CEG_DAL.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Mvc.Rendering;
using CEG_RazorWebApp.Libraries;

namespace CEG_RazorWebApp.Models.Homework.Create
{
    public class CreateHomeworkVM
    {
        public CreateHomeworkVM()
        {
            var lib = new ChildrenEnglishGameLibrary();
            Hours = 1;
            Type = Constants.HOMEWORK_TYPE_VOCAB;
            DefaultHomeworkTypeSelectList = lib.GetHomeworkTypeSelectableList(Type);
        }
        [Required(ErrorMessage = "Homework Title is required")]
        [DisplayName("Homework Title")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [DisplayName("Description")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Hours is required")]
        [Range(1, int.MaxValue)]
        [DisplayName("Hours")]
        public int? Hours { get; set; }
        public string? Type { get; set; }
        public int? SessionId { get; set; }
        public string? SessionTitle { get; set; }
        public List<SelectListItem> DefaultHomeworkTypeSelectList { get; set; }
    }
}
