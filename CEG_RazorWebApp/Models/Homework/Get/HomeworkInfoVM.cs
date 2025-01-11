using CEG_BAL.ViewModels;
using CEG_RazorWebApp.Models.HomeworkQuestion.Get;

namespace CEG_RazorWebApp.Models.Homework.Get
{
    public class HomeworkInfoVM
    {
        public HomeworkInfoVM()
        {
        }
        public int? HomeworkId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int? Hours { get; set; }
        public string? Type { get; set; }
        public string? Status { get; set; }
        public int? QuestionsAmount { get; set; }
    }
}
