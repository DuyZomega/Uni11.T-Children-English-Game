namespace CEG_WebMVC.Models.ViewModels.Admin
{
    public class AdminProfileVM
    {
        public AdminProfileVM()
        {
            adminPassword = new CEG_BAL.ViewModels.Account.UpdateAccountPassword();
            adminDetail = new CEG_BAL.ViewModels.AccountViewModel();
        }

        public CEG_BAL.ViewModels.Account.UpdateAccountPassword adminPassword { get; set; }

        public CEG_BAL.ViewModels.AccountViewModel adminDetail { get; set; }
    }
}
