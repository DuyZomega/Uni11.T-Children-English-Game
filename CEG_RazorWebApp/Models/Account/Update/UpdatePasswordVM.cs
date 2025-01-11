using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CEG_RazorWebApp.Models.Account.Update
{
    public class UpdatePasswordVM
    {
        [Required(ErrorMessage = "Account Id is required")]
        public int? AccountId { get; set; }
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
    }
}
