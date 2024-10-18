using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CEG_RazorWebApp.Models.Student.Update
{
    public class UpdateStudentVM
    {
        [Required(ErrorMessage = "Required Age is required")]
        [Range(11, 18)]
        [DisplayName("Age Require")]
        public int? Age { get; set; }

        public DateTime? Birthdate { get; set; }

        public string? Image { get; set; }
    }
}
