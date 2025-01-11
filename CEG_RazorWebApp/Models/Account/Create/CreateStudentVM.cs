using System.ComponentModel;
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
        [Required(ErrorMessage = "Description is required")]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Birth Date is required")]
        [DisplayName("Birth Date")]
        [DataType(DataType.DateTime)]
        public DateTime Birthdate { get; set; }

        [Required(ErrorMessage = "Parent's Account Fullname is required")]
        [DisplayName("Parent's Account Fullname")]
        public string ParentFullname { get; set; }

        public virtual CreateAccountVM Account { get; set; }
    }
}
