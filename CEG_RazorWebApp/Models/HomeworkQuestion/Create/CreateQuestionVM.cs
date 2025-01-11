namespace CEG_RazorWebApp.Models.HomeworkQuestion.Create
{
    public class CreateQuestionVM
    {
        public CreateQuestionVM()
        {
            Question = "";
        }
        public string? Question { get; set; }
        public int? HomeworkId { get; set; }
        public string? HomeworkTitle { get; set; }
    }
}
