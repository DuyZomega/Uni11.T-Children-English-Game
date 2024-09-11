using CEG_WebMVC.Library;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace CEG_WebMVC.Models.ViewModels.Account.Get
{
    public class AccountStatusVM
    {
        public AccountStatusVM()
        {
            ChildrenEnglishGameLibrary lib = new ChildrenEnglishGameLibrary();
            DefaultAccountStatusSelectList = lib.GetAccountStatusSelectableList(Status.IsNullOrEmpty() ? Constants.ACCOUNT_STATUS_INACTIVE : Status);
        }
        public string? AccountId { get; set; }
        [Required(ErrorMessage = "Account Username is required")]
        [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Username is invalid")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Full Name is required")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Fullname is invalid")]
        public string FullName { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? CreatedDate { get; set; }
        public string? Gender { get; set; }
        public string? Role { get; set; }
        public string? Status { get; set; }
        public List<SelectListItem> DefaultAccountStatusSelectList { get; set; }
    }
}
