using Microsoft.AspNetCore.Mvc.Rendering;

namespace CEG_WebMVC.Models.ViewModels.Admin
{
    public class AdminProfileVM
    {
        public AdminProfileVM()
        {
            adminPassword = new CEG_BAL.ViewModels.Account.UpdateAccountPassword();
            adminDetail = new CEG_BAL.ViewModels.AccountViewModel();
            DefaultUserGenderSelectList = new List<SelectListItem>() {
                new SelectListItem() { Text = "Gender", Value = ""},
                new SelectListItem { Text = "Male", Value = "Male" },
                new SelectListItem { Text = "Female", Value = "Female" },
                new SelectListItem { Text = "Other", Value = "Other" },
            };
        }

        public CEG_BAL.ViewModels.Account.UpdateAccountPassword adminPassword { get; set; }

        public CEG_BAL.ViewModels.AccountViewModel adminDetail { get; set; }
        public List<SelectListItem> DefaultUserGenderSelectList { get; set; }
    }
}
