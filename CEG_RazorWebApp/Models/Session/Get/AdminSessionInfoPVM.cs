using CEG_RazorWebApp.Models.Homework.Create;
using CEG_RazorWebApp.Models.Homework.Get;
using CEG_RazorWebApp.Models.Session.Update;

namespace CEG_RazorWebApp.Models.Session.Get
{
    public class AdminSessionInfoPVM
    {
        public AdminSessionInfoPVM(
            int? courseId,
            SessionInfoVM? session = null,
            UpdateSessionVM? updateSessionInfo = null)
        {
            CourseId = courseId;
            SessionInfo = session ?? new SessionInfoVM();
            UpdateSessionInfo = updateSessionInfo ?? new UpdateSessionVM();
        }
        public int? CourseId { get; set; }
        public SessionInfoVM? SessionInfo { get; set; }
        public UpdateSessionVM? UpdateSessionInfo { get; set; }
    }
}
