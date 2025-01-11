using CEG_RazorWebApp.Models.Account.Update;
using System.ComponentModel.DataAnnotations;

namespace CEG_RazorWebApp.Models.Teacher.Update
{
    public class UpdateTeacherVM
    {
        [Phone(ErrorMessage = "Please enter a valid Phone No")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone Number is required")]
        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Address is required")]
        [DataType(DataType.MultilineText)]
        public string? Address { get; set; }

        public string? Image { get; set; }

        public UpdateAccountVM? Account { get; set; } = new UpdateAccountVM();
    }
}
