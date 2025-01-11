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
            UpdateHomeworkVM? updateHomeworkInfo = null
            )
        {
            CourseId = courseId;
            SessionId = sessionId;
            HomeworkInfo = homework ?? new HomeworkInfoVM();
            UpdateHomeworkInfo = updateHomeworkInfo ?? new UpdateHomeworkVM();
        }
        public int? CourseId { get; set; }
        public int? SessionId { get; set; }
        public HomeworkInfoVM? HomeworkInfo { get; set; }
        public UpdateHomeworkVM? UpdateHomeworkInfo { get; set; }
    }
}
