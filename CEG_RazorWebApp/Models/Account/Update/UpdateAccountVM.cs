using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CEG_RazorWebApp.Models.Account.Update
{
    public class UpdateAccountVM
    {
        /*[Required(AllowEmptyStrings = false, ErrorMessage = "Account Username is required")]
        [StringLength(20, ErrorMessage = "Username must have more than or equal 6 characters and less than or equal 20 characters", MinimumLength = 6)]
        [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Username is invalid")]
        public string? Username { get; set; }*/

        [Required(AllowEmptyStrings = false, ErrorMessage = "Full Name is required")]
        [StringLength(50, ErrorMessage = "Full Name must have more than or equal 6 characters and less than or equal 50 characters", MinimumLength = 6)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Fullname is invalid")]
        public string? Fullname { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select a gender")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Gender is invalid")]
        public string? Gender { get; set; }
    }
}
