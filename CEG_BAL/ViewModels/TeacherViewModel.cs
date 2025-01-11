using System.ComponentModel.DataAnnotations;

namespace CEG_BAL.ViewModels
{
    public class TeacherViewModel
    {
        public TeacherViewModel()
        {
            Account = new AccountViewModel();
        }
        public int? AccountId { get; set; }
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Email is invalid")]
        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string Certificate { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string? Image { get; set; }

        public AccountViewModel Account { get; set; } = null!;

        public List<ClassViewModel> Classes { get; set; } = new List<ClassViewModel>();

        public List<CourseViewModel> Courses { get; set; } = new List<CourseViewModel>();
    }
}
