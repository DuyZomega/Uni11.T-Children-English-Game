using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels.Account.Get
{
    public class GetAccountStatus
    {
        public GetAccountStatus()
        {
            DefaultAccountStatusSelectList = new List<string>() {"Active", "Inactive","Expired","Denied","Suspended"};
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
        public List<string> DefaultAccountStatusSelectList { get; set; }
    }
}
