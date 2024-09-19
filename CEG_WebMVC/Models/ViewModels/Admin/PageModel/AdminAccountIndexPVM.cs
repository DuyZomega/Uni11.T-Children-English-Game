using CEG_WebMVC.Models.ViewModels.Account.Create;
using CEG_WebMVC.Models.ViewModels.Account.Get;

namespace CEG_WebMVC.Models.ViewModels.Course.PageModel
{
    public class AdminAccountIndexPVM
    {
        public AdminAccountIndexPVM()
        {
            AccountStatuses = new List<AccountStatusVM>();
            createTeacher = new CreateTeacherVM();
            createParent = new CreateParentVM();
            createStudent = new CreateStudentVM();
        }
        public List<AccountStatusVM> AccountStatuses { get; set; }
        public CreateTeacherVM createTeacher { get; set; }
        public CreateParentVM createParent { get; set; }
        public CreateStudentVM createStudent { get; set; }
    }
}
