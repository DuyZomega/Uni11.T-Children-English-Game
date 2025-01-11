using CEG_RazorWebApp.Libraries;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace CEG_RazorWebApp.Models.Account.Get
{
    public class AccountStatusVM
    {
        public AccountStatusVM()
        {
            CEG_RAZOR_Library lib = new CEG_RAZOR_Library();
            DefaultAccountStatusSelectList = lib.GetAccountStatusSelectableList(Status.IsNullOrEmpty() ? Constants.ACCOUNT_STATUS_INACTIVE : Status);
        }
        public string? AccountId { get; set; }
        [Required(ErrorMessage = "Account Username is required")]
        [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Username is invalid")]
        public string UserName { get; set; } = null!;
        [Required(ErrorMessage = "Full Name is required")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Fullname is invalid")]
        public string FullName { get; set; } = null!;
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }
        public string? Gender { get; set; }
        public string? Role { get; set; }
        public string? Status { get; set; }
        public List<SelectListItem> DefaultAccountStatusSelectList { get; set; }
    }
}
