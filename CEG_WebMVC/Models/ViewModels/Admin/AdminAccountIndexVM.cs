using CEG_BAL.ViewModels.Account.Create;
using CEG_WebMVC.Models.ViewModels.Account.Create;

namespace CEG_WebMVC.Models.ViewModels.Admin
{
    public class AdminAccountIndexVM
    {
        public AdminAccountIndexVM()
        {
            AccountStatuses = new List<Account.AccountStatusVM>();
            SelectedAccountStatuses = new List<string>();
            createTeacher = new CreateTeacherVM();
            createParent = new CreateNewParent();
            createStudent = new CreateNewStudent();
        }
        public List<Account.AccountStatusVM> AccountStatuses { get; set; }

        public List<string> SelectedAccountStatuses { get; set; }

        public CreateTeacherVM createTeacher { get; set; }
        public CreateNewParent createParent { get; set; }
        public CreateNewStudent createStudent { get; set; }
    }
}
