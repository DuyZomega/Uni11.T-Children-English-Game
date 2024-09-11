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
                /*AccountStatuses = new List<Account.AccountStatusVM>() {
                    new Account.AccountStatusVM() { AccountId = "yomamam", FullName= "asdsadadsad", Role= "parent", Status= Constants.ACCOUNT_STATUS_ACTIVE, UserName= "no"},
                    new Account.AccountStatusVM() { AccountId = "yomamam", FullName= "asdsadadsad", Role= "parent", Status= Constants.ACCOUNT_STATUS_EXPIRED, UserName= "no"},
                    new Account.AccountStatusVM() { AccountId = "yomamam", FullName= "asdsadadsad", Role= "parent", Status= Constants.ACCOUNT_STATUS_ACTIVE, UserName= "no"},
                    new Account.AccountStatusVM() { AccountId = "yomamam", FullName= "asdsadadsad", Role= "parent", Status= Constants.ACCOUNT_STATUS_ACTIVE, UserName= "no"},
                    new Account.AccountStatusVM() { AccountId = "yomamam", FullName= "asdsadadsad", Role= "parent", Status= "", UserName= "no"}
                };*/
                //SelectedAccountStatuses = new List<string>();
            createTeacher = new CreateTeacherVM();
            createParent = new CreateParentVM();
            createStudent = new CreateNewStudent();
        }
        public List<AccountStatusVM> AccountStatuses { get; set; }

        //public List<string> SelectedAccountStatuses { get; set; }

        public CreateTeacherVM createTeacher { get; set; }
        public CreateParentVM createParent { get; set; }
        public CreateNewStudent createStudent { get; set; }
    }
}
