using CEG_BAL.ViewModels;
using CEG_WebMVC.Models.ViewModels.Course.Get;
using CEG_WebMVC.Models.ViewModels.Course.Update;
using CEG_WebMVC.Models.ViewModels.Homework.Create;
using CEG_WebMVC.Models.ViewModels.Session.Create;
using CEG_WebMVC.Models.ViewModels.Session.Get;
using CEG_WebMVC.Models.ViewModels.Session.Update;

namespace CEG_WebMVC.Models.ViewModels.Admin.PageModel
{
    public class AdminCourseInfoPVM
    {
        public AdminCourseInfoPVM(
            CourseInfoVM? courseInfo = null,
            UpdateCourseVM? updateCourseInfo = null,
            List<AdminSessionInfoPVM>? sessions = null,
            CreateSessionVM? createSession = null)
        {

            CourseInfo = courseInfo ?? new CourseInfoVM();
            UpdateCourseInfo = updateCourseInfo ?? new UpdateCourseVM();
            Sessions = sessions ?? new List<AdminSessionInfoPVM>();
            CreateSession = createSession ?? new CreateSessionVM(CourseInfo.CourseName);
        }
        public CourseInfoVM? CourseInfo { get; set; }
        public UpdateCourseVM? UpdateCourseInfo { get; set; }
        public CreateSessionVM? CreateSession { get; set; }
        public List<AdminSessionInfoPVM>? Sessions { get; set; }
    }
}
