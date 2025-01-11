namespace CEG_RazorWebApp.Models.HomeworkQuestion.Update
{
    public class UpdateQuestionVM
    {
        public UpdateQuestionVM()
        {
            Question = "";
        }
        public int? HomeworkQuestionId { get; set; }
        public string? Question { get; set; }
        public int? HomeworkId { get; set; }
    }
}
