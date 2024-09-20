using CEG_BAL.ViewModels;
using CEG_WebMVC.Models.ViewModels.Course.Get;
using CEG_WebMVC.Models.ViewModels.Course.Update;
using CEG_WebMVC.Models.ViewModels.Session.Create;
using CEG_WebMVC.Models.ViewModels.Session.Get;

namespace CEG_WebMVC.Models.ViewModels.Course.PageModel
{
    public class AdminCourseInfoPVM
    {
        public AdminCourseInfoPVM(CourseInfoVM? courseInfo = null, UpdateCourseVM? updateCourseInfo = null, List<SessionInfoVM>? sessions = null)
        {
            CourseInfo = courseInfo ?? new CourseInfoVM();
            UpdateCourseInfo = updateCourseInfo ?? new UpdateCourseVM();
            Sessions = sessions ?? new List<SessionInfoVM>();
        }
        public CourseInfoVM? CourseInfo;
        public UpdateCourseVM? UpdateCourseInfo;
        public List<SessionInfoVM>? Sessions { get; set; }
    }
}
