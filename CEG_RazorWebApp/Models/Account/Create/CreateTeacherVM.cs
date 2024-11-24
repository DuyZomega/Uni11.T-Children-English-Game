using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CEG_RazorWebApp.Models.Account.Create
{
    public class CreateTeacherVM
    {
        public CreateTeacherVM()
        {
            Account = new CreateAccountVM();
        }
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Email is invalid")]

        public string? Email { get; set; }
        [Phone(ErrorMessage = "Please enter a valid Phone No")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone Number is required")]
        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Address is required")]
        [DataType(DataType.MultilineText)]
        public string? Address { get; set; }
        [Required(ErrorMessage = "Certificate Image is required")]
        [DisplayName("Certificate Image File")]
        public IFormFile? ImageUpload { get; set; }

        public CreateAccountVM Account { get; set; }
    }
}
