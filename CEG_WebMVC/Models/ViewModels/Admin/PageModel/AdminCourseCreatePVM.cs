using CEG_WebMVC.Models.ViewModels.Course.Create;
using CEG_WebMVC.Models.ViewModels.Session.Create;

namespace CEG_WebMVC.Models.ViewModels.Course.PageModel
{
    public class AdminCourseCreatePVM
    {
        public AdminCourseCreatePVM()
        {
            CreateCourse = new CreateCourseVM();
            CreateSessions = new List<CreateSessionVM> { new CreateSessionVM() };
        }

        public CreateCourseVM? CreateCourse { get; set; }
        public List<CreateSessionVM>? CreateSessions { get; set; }
    }
}
