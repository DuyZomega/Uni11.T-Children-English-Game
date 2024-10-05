using CEG_RazorWebApp.Models.HomeworkAnswer.Get;

namespace CEG_RazorWebApp.Models.HomeworkQuestion.Get
{
    public class QuestionInfoVM
    {
        public int? HomeworkQuestionId { get; set; }
        public string? Question { get; set; }
        public int? AnswersAmount { get; set; }
    }
}
