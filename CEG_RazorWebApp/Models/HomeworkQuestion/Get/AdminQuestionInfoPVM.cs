using CEG_RazorWebApp.Models.HomeworkQuestion.Update;

namespace CEG_RazorWebApp.Models.HomeworkQuestion.Get
{
    public class AdminQuestionInfoPVM
    {
        public AdminQuestionInfoPVM(
            int? courseId,
            int? sessionId,
            int? homeworkId,
            QuestionInfoVM? question = null,
            UpdateQuestionVM? updateQuestionInfo = null
            )
        {
            CourseId = courseId;
            SessionId = sessionId;
            HomeworkId = homeworkId;
            QuestionInfo = question ?? new QuestionInfoVM();
            UpdateQuestionInfo = updateQuestionInfo ?? new UpdateQuestionVM();
        }
        public int? CourseId { get; set; }
        public int? SessionId { get; set; }
        public int? HomeworkId { get; set; }
        public string? HomeworkStatus { get; set; }
        public QuestionInfoVM? QuestionInfo { get; set; }
        public UpdateQuestionVM? UpdateQuestionInfo { get; set; }
    }
}
