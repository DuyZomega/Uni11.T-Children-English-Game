using CEG_RazorWebApp.Models.HomeworkAnswer.Update;
using CEG_RazorWebApp.Models.HomeworkQuestion.Get;
using CEG_RazorWebApp.Models.HomeworkQuestion.Update;

namespace CEG_RazorWebApp.Models.HomeworkAnswer.Get
{
    public class AdminAnswerInfoPVM
    {
        public AdminAnswerInfoPVM(
            int? courseId, 
            int? sessionId, 
            int? homeworkId, 
            int? questionId,
            string? homeworkStatus,
            AnswerInfoVM? answerInfo = null, 
            UpdateAnswerVM? updateAnswerInfo = null
            )
        {
            CourseId = courseId;
            SessionId = sessionId;
            HomeworkId = homeworkId;
            QuestionId = questionId;
            HomeworkStatus = homeworkStatus;
            AnswerInfo = answerInfo ?? new AnswerInfoVM();
            UpdateAnswerInfo = updateAnswerInfo ?? new UpdateAnswerVM();
        }

        public int? CourseId { get; set; }
        public int? SessionId { get; set; }
        public int? HomeworkId { get; set; }
        public int? QuestionId { get; set; }
        public string? HomeworkStatus { get; set; }
        public AnswerInfoVM? AnswerInfo { get; set; }
        public UpdateAnswerVM? UpdateAnswerInfo { get; set; }
    }
}
