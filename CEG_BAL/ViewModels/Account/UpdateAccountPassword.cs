using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels.Account
{
    public class UpdateAccountPassword
    {
        [Required]
        public int AccountId { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Password is invalid")]
        [DataType(DataType.Password)]
        [PasswordPropertyText]
        public string Password { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "New Password is invalid")]
        [DataType(DataType.Password)]
        [PasswordPropertyText]
        public string NewPassword { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "New Confirm Password is invalid")]
        [DataType(DataType.Password)]
        [PasswordPropertyText]
        public string NewConfirmPassword { get; set; }
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Email is invalid")]
        public string? Email { get; set; }
    }
}
