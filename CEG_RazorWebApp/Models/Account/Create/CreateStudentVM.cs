using System.ComponentModel.DataAnnotations;
using CEG_BAL.ViewModels.Account.Create;

namespace CEG_RazorWebApp.Models.Account.Create
{
    public class CreateStudentVM
    {
        public CreateStudentVM()
        {
            Account = new CreateAccountVM();
            Birthdate = DateTime.Now.AddYears(-16);
        }
        public string Description { get; set; }
        [Required(ErrorMessage = "Birth Date is required")]
        [DisplayName("Birth Date")]
        [DataType(DataType.DateTime)]
        public DateTime Birthdate { get; set; }
        [Required(ErrorMessage = "Parent's Account Username is required")]
        [DisplayName("Parent's Account Username")]
        public string ParentUsername { get; set; }
        public virtual CreateAccountVM Account { get; set; }
    }
}
