using CEG_BAL.ViewModels;

using CEG_WebMVC.Models.ViewModels.Homework.Get;
using CEG_WebMVC.Models.ViewModels.Homework.Update;
using CEG_WebMVC.Models.ViewModels.Session.Get;
using CEG_WebMVC.Models.ViewModels.Session.Update;

namespace CEG_WebMVC.Models.ViewModels.Admin.PageModel
{
    public class AdminHomeworkInfoPVM
    {
        public AdminHomeworkInfoPVM(
            int? sessionId,
            HomeworkInfoVM? homework = null,
            UpdateHomeworkVM? updateHomeworkInfo = null,
            //CreateHomeworkVM? createHomework = null,
            List<HomeworkQuestionViewModel>? questions = null)
        {
            SessionId = sessionId;
            HomeworkInfo = homework ?? new HomeworkInfoVM();
            UpdateHomeworkInfo = updateHomeworkInfo ?? new UpdateHomeworkVM();
            //CreateQuestion = createHomework != null && createHomework.SessionId.Equals(SessionInfo.SessionId) ? createHomework : new CreateHomeworkVM();
            Questions = questions ?? new List<HomeworkQuestionViewModel>();
        }
        public int? SessionId { get; set; }
        public HomeworkInfoVM? HomeworkInfo { get; set; }
        public UpdateHomeworkVM? UpdateHomeworkInfo { get; set; }
        //public CreateHWQuestionVM? CreateQuestion { get; set; }
        public List<HomeworkQuestionViewModel>? Questions { get; set; }
    }
}
