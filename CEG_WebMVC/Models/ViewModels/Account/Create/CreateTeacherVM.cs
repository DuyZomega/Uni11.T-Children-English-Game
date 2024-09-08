namespace CEG_WebMVC.Models.ViewModels.Account.Create
{
    public class CreateTeacherVM
    {
        public CreateTeacherVM()
        {
            Account = new CreateAccountVM();
        }
        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public virtual CreateAccountVM Account { get; set; }
    }
}
