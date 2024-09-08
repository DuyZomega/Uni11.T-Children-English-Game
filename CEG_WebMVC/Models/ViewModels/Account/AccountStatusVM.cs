using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CEG_WebMVC.Models.ViewModels.Account
{
    public class AccountStatusVM
    {
        public AccountStatusVM()
        {
            DefaultAccountStatusSelectList = new List<SelectListItem>() {
                new SelectListItem { Text = "Active", Value = "Active", Selected = true },
                new SelectListItem { Text = "Inactive", Value = "Inactive" },
                new SelectListItem { Text = "Expired", Value = "Expired" },
                new SelectListItem { Text = "Denied", Value = "Denied" },
                new SelectListItem { Text = "Suspended", Value = "Suspended" }
            };
        }
        public string? MemberId { get; set; }
        [Required(ErrorMessage = "Account Username is required")]
        [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Username is invalid")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Full Name is required")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Fullname is invalid")]
        public string FullName { get; set; }
        [DataType(DataType.DateTime)]
        public string? Role { get; set; }
        public string? Status { get; set; }
        public List<SelectListItem> DefaultAccountStatusSelectList { get; set; }
    }
}
