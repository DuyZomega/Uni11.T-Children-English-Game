using CEG_BAL.ViewModels;
using CEG_WebMVC.Models.ViewModels.Course.Get;
using CEG_WebMVC.Models.ViewModels.Course.Update;
using CEG_WebMVC.Models.ViewModels.Homework.Create;
using CEG_WebMVC.Models.ViewModels.Session.Create;
using CEG_WebMVC.Models.ViewModels.Session.Get;

namespace CEG_WebMVC.Models.ViewModels.Admin.PageModel
{
    public class AdminCourseInfoPVM
    {
        public AdminCourseInfoPVM(
            CourseInfoVM? courseInfo = null,
            UpdateCourseVM? updateCourseInfo = null,
            List<SessionInfoVM>? sessions = null,
            CreateSessionVM? createSession = null,
            CreateHomeworkVM? createHomework = null)
        {

            CourseInfo = courseInfo ?? new CourseInfoVM();
            UpdateCourseInfo = updateCourseInfo ?? new UpdateCourseVM();
            Sessions = sessions ?? new List<SessionInfoVM>();
            CreateSession = createSession ?? new CreateSessionVM(CourseInfo.CourseId, CourseInfo.CourseName);
            CreateHomework = createHomework ?? new CreateHomeworkVM();
        }
        public CourseInfoVM? CourseInfo { get; set; }
        public UpdateCourseVM? UpdateCourseInfo { get; set; }
        public CreateSessionVM? CreateSession { get; set; }
        public CreateHomeworkVM? CreateHomework { get; set; }
        public List<SessionInfoVM>? Sessions { get; set; }
    }
}
