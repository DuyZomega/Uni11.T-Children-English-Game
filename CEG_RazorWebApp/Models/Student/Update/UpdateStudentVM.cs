using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using CEG_RazorWebApp.Models.Account.Update;

namespace CEG_RazorWebApp.Models.Student.Update
{
    public class UpdateStudentVM
    {
        [Required(ErrorMessage = "Description is required")]
        [DisplayName("Description")]
        public string? Description { get; set; }
        /*[Required(ErrorMessage = "Required Age is required")]
        [Range(11, 18)]
        [DisplayName("Age Require")]
        public int? Age { get; set; }*/
        [Required(ErrorMessage = "Birthdate is required")]
        [DisplayName("Birthdate")]
        [DataType(DataType.DateTime)]
        public DateTime? Birthdate { get; set; }
        [Required(ErrorMessage = "Parent's Fullname is required")]
        [DisplayName("Parent's Fullname")]
        public string? ParentFullname { get; set; }
        public string? Image { get; set; }
        public UpdateAccountVM? Account { get; set; } = new UpdateAccountVM();
    }
}
