using CEG_BAL.ViewModels.Account.Create;
using CEG_WebMVC.Library;
using CEG_WebMVC.Models.ViewModels.Account.Create;
using CEG_WebMVC.Models.ViewModels.Account.Get;

namespace CEG_WebMVC.Models.ViewModels.Admin.Get
{
    public class AdminAccountIndexVM
    {
        public AdminAccountIndexVM()
        {
            AccountStatuses = new List<AccountStatusVM>();
            createTeacher = new CreateTeacherVM();
            createParent = new CreateParentVM();
            createStudent = new CreateNewStudent();
        }
        public List<AccountStatusVM> AccountStatuses { get; set; }
        public CreateTeacherVM createTeacher { get; set; }
        public CreateParentVM createParent { get; set; }
        public CreateNewStudent createStudent { get; set; }
    }
}
