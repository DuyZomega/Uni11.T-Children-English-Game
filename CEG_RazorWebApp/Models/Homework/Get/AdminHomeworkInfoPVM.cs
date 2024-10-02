using CEG_BAL.ViewModels;
using CEG_RazorWebApp.Models.Homework.Update;

namespace CEG_RazorWebApp.Models.Homework.Get
{
    public class AdminHomeworkInfoPVM
    {
        public AdminHomeworkInfoPVM(
            int? courseId,
            int? sessionId,
            HomeworkInfoVM? homework = null,
            UpdateHomeworkVM? updateHomeworkInfo = null,
            //CreateHomeworkVM? createHomework = null,
            List<HomeworkQuestionViewModel>? questions = null)
        {
            CourseId = courseId;
            SessionId = sessionId;
            HomeworkInfo = homework ?? new HomeworkInfoVM();
            UpdateHomeworkInfo = updateHomeworkInfo ?? new UpdateHomeworkVM();
            //CreateQuestion = createHomework != null && createHomework.SessionId.Equals(SessionInfo.SessionId) ? createHomework : new CreateHomeworkVM();
            Questions = questions ?? new List<HomeworkQuestionViewModel>();
        }
        public int? CourseId { get; set; }
        public int? SessionId { get; set; }
        public HomeworkInfoVM? HomeworkInfo { get; set; }
        public UpdateHomeworkVM? UpdateHomeworkInfo { get; set; }
        //public CreateHWQuestionVM? CreateQuestion { get; set; }
        public List<HomeworkQuestionViewModel>? Questions { get; set; }
    }
}
