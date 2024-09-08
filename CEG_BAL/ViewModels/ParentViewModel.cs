namespace CEG_BAL.ViewModels
{
    public class ParentViewModel
    {
        public string Email { get; set; } = null!;

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public virtual AccountViewModel Account { get; set; } = null!;

        public virtual ICollection<PaymentViewModel> Payments { get; set; } = new List<PaymentViewModel>();

        public virtual ICollection<StudentViewModel> Students { get; set; } = new List<StudentViewModel>();
    }
}
