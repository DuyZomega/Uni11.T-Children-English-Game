using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels.Admin.Update
{
    public class UpdateAccount
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Full Name is required")]
        [StringLength(50, ErrorMessage = "Full Name must have more than or equal 6 characters and less than or equal 50 characters", MinimumLength = 6)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Fullname is invalid")]
        public string? Fullname { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select a gender")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Gender is invalid")]
        public string? Gender { get; set; }
    }
}
