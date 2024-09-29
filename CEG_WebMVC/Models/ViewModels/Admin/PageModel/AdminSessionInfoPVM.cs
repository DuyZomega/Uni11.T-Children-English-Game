using CEG_WebMVC.Models.ViewModels.Course.Get;
using CEG_WebMVC.Models.ViewModels.Course.Update;
using CEG_WebMVC.Models.ViewModels.Homework.Create;
using CEG_WebMVC.Models.ViewModels.Session.Create;
using CEG_WebMVC.Models.ViewModels.Session.Get;
using CEG_WebMVC.Models.ViewModels.Session.Update;

namespace CEG_WebMVC.Models.ViewModels.Admin.PageModel
{
    public class AdminSessionInfoPVM
    {
        public AdminSessionInfoPVM(
            int? courseId,
            SessionInfoVM? session = null,
            UpdateSessionVM? updateSessionInfo = null,
            List<AdminHomeworkInfoPVM>? homeworks = null,
            CreateHomeworkVM? createHomework = null)
        {
            CourseId = courseId;
            SessionInfo = session ?? new SessionInfoVM();
            UpdateSessionInfo = updateSessionInfo ?? new UpdateSessionVM();
            CreateHomework = createHomework != null && createHomework.SessionId.Equals(SessionInfo.SessionId) ? createHomework : new CreateHomeworkVM();
            Homeworks = homeworks ?? new List<AdminHomeworkInfoPVM>();
        }
        public int? CourseId { get; set; }
        public SessionInfoVM? SessionInfo {  get; set; }
        public UpdateSessionVM? UpdateSessionInfo { get; set; }
        public CreateHomeworkVM? CreateHomework { get; set; }
        public List<AdminHomeworkInfoPVM>? Homeworks { get; set; }
    }
}
