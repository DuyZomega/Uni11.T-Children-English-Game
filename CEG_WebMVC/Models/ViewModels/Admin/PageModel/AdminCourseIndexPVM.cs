using CEG_WebMVC.Models.ViewModels.Account.Create;
using CEG_WebMVC.Models.ViewModels.Account.Get;
using CEG_WebMVC.Models.ViewModels.Course.Create;
using CEG_WebMVC.Models.ViewModels.Course.Get;

namespace CEG_WebMVC.Models.ViewModels.Admin.PageModel
{
    public class AdminCourseIndexPVM
    {
        public AdminCourseIndexPVM()
        {
            CreateCourse = new CreateCourseVM();
            Courses = new List<IndexCourseInfoVM>();
        }
        public CreateCourseVM? CreateCourse { get; set; }
        public List<IndexCourseInfoVM> Courses { get; set; }
    }
}
