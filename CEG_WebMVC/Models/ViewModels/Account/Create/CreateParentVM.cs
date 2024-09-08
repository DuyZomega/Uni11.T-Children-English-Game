using CEG_BAL.ViewModels;

namespace CEG_WebMVC.Models.ViewModels.Account.Create
{
    public class CreateParentVM
    {
        public CreateParentVM()
        {
            Account = new CreateAccountVM();
        }
        public string Email { get; set; } = null!;

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public virtual CreateAccountVM Account { get; set; } = null!;
    }
}
