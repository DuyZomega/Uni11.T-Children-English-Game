using CEG_BAL.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CEG_WebMVC.Models.ViewModels.Account.Create
{
    public class CreateAccountVM
    {
        public CreateAccountVM()
        {
            DefaultAccountGenderSelectList = new List<SelectListItem>() {
                new SelectListItem { Text = "Gender", Value = "Gender", Selected = true },
                new SelectListItem { Text = "Male", Value = "Male" },
                new SelectListItem { Text = "Female", Value = "Female" },
                new SelectListItem { Text = "Other", Value = "Other" }
            };
        }
        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; }

        public string Fullname { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public string Gender { get; set; }
        public List<SelectListItem> DefaultAccountGenderSelectList { get; set; }

        public string Status { get; set; } = null!;

    }
}
