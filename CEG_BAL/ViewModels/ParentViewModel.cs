using System.ComponentModel.DataAnnotations;

namespace CEG_BAL.ViewModels
{
    public class ParentViewModel
    {
        public ParentViewModel()
        {
            Account = new AccountViewModel();
        }
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Email is invalid")]
        public string Email { get; set; } = null!;

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public AccountViewModel Account { get; set; } = null!;

        public List<TransactionViewModel> Transactions { get; set; } = new List<TransactionViewModel>();

        public List<StudentViewModel> Students { get; set; } = new List<StudentViewModel>();
    }
}
