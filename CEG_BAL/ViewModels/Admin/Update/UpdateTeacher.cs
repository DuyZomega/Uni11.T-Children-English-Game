using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels.Admin.Update
{
    public class UpdateTeacher
    {
        [Phone(ErrorMessage = "Please enter a valid Phone No")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone Number is required")]
        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Address is required")]
        [DataType(DataType.MultilineText)]
        public string? Address { get; set; }

        public string? Image { get; set; }

        public UpdateAccount? Account { get; set; } = new UpdateAccount();
    }
}
