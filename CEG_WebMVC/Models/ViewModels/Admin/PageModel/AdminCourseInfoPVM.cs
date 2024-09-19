using CEG_BAL.ViewModels;
using CEG_WebMVC.Models.ViewModels.Course.Get;
using CEG_WebMVC.Models.ViewModels.Session.Create;

namespace CEG_WebMVC.Models.ViewModels.Course.PageModel
{
    public class AdminCourseInfoPVM
    {
        public AdminCourseInfoPVM()
        {
            CourseInfo = new CourseInfoVM();
            Sessions = new List<SessionViewModel>();
        }
        public CourseInfoVM? CourseInfo;
        public List<SessionViewModel>? Sessions { get; set; }
    }
}
