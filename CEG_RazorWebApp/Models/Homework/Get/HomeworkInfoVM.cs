using CEG_BAL.ViewModels;
using CEG_RazorWebApp.Models.HomeworkQuestion.Get;

namespace CEG_RazorWebApp.Models.Homework.Get
{
    public class HomeworkInfoVM
    {
        public HomeworkInfoVM()
        {
            QuestionsAmount = Questions.Count;
        }
        public int? HomeworkId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int? Hours { get; set; }
        public string? Status { get; set; }
        public int? QuestionsAmount { get; set; }
        public List<QuestionInfoVM> Questions { get; set; } = new List<QuestionInfoVM>();
    }
}
