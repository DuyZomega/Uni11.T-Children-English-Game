using CEG_WebMVC.Models.ViewModels.Class.Create;
using CEG_WebMVC.Models.ViewModels.Class.Get;

namespace CEG_WebMVC.Models.ViewModels.Admin.PageModel
{
    public class AdminClassIndexPVM
    {
        public AdminClassIndexPVM()
        {
            Classes = new List<IndexClassInfoVM>();
            CreateClass = new CreateClassVM();
        }
        public List<IndexClassInfoVM> Classes { get; set; }
        public CreateClassVM CreateClass { get; set; }
    }
}
