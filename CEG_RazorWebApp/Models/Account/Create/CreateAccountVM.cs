using CEG_RazorWebApp.Libraries;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CEG_RazorWebApp.Models.Account.Create
{
    public class CreateAccountVM
    {
        public CreateAccountVM()
        {
            ChildrenEnglishGameLibrary lib = new ChildrenEnglishGameLibrary();
            DefaultAccountGenderSelectList = lib.GetGenderSelectableList(Constants.GENDER_TITLE);
            Status = Constants.ACCOUNT_STATUS_ACTIVE;
            DefaultAccountStatusSelectList = lib.GetAccountStatusSelectableList(Status);
            CreatedDate = DateTime.Now;

        }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Account Username is required")]
        [StringLength(20, ErrorMessage = "Username must have more than or equal 6 characters and less than or equal 20 characters", MinimumLength = 6)]
        [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Username is invalid")]
        public string? Username { get; set; }
        [PasswordPropertyText]
        [DataType(DataType.Password)]
        [StringLength(maximumLength: int.MaxValue, ErrorMessage = "Password must have more than or equal 8 characters", MinimumLength = 8)]
        [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Password is invalid")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        public string? Password { get; set; }
        [PasswordPropertyText]
        [Compare(otherProperty: "Password", ErrorMessage = "Password and Confirmation Password must match.")]
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Confirm Password is required")]
        public string? ConfirmPassword { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Full Name is required")]
        [StringLength(50, ErrorMessage = "Full Name must have more than or equal 6 characters and less than or equal 50 characters", MinimumLength = 6)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Fullname is invalid")]
        public string? Fullname { get; set; }

        public DateTime CreatedDate { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select a gender")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Gender is invalid")]
        public string? Gender { get; set; }
        public List<SelectListItem> DefaultAccountGenderSelectList { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select a status")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Status is invalid")]
        public string? Status { get; set; }
        public List<SelectListItem> DefaultAccountStatusSelectList { get; set; }

    }
}
