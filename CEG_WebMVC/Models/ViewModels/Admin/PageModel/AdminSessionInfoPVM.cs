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
            CreateHomeworkVM? createHomework = null)
        {
            CourseId = courseId;
            Session = session ?? new SessionInfoVM();
            UpdateSessionInfo = updateSessionInfo ?? new UpdateSessionVM();
            CreateHomework = createHomework != null && createHomework.SessionId.Equals(Session.SessionId) ? createHomework : new CreateHomeworkVM();
        }
        public int? CourseId { get; set; }
        public SessionInfoVM? Session {  get; set; }
        public UpdateSessionVM? UpdateSessionInfo { get; set; }
        public CreateHomeworkVM? CreateHomework { get; set; }
    }
}
