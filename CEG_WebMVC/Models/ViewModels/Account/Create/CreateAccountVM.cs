using CEG_BAL.ViewModels;
using CEG_WebMVC.Library;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CEG_WebMVC.Models.ViewModels.Account.Create
{
    public class CreateAccountVM
    {
        public CreateAccountVM()
        {
            ChildrenEnglishGameLibrary lib = new ChildrenEnglishGameLibrary();
            DefaultAccountGenderSelectList = lib.GetGenderSelectableList(Constants.GENDER_TITLE);
            DefaultAccountStatusSelectList = lib.GetAccountStatusSelectableList(Constants.ACCOUNT_STATUS_TITLE);

        }
        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; }

        public string Fullname { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public string Gender { get; set; }
        public List<SelectListItem> DefaultAccountGenderSelectList { get; set; }

        public string Status { get; set; } = null!;
        public List<SelectListItem> DefaultAccountStatusSelectList { get; set; }

    }
}
