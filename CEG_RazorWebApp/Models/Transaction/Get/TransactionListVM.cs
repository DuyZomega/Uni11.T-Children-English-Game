namespace CEG_RazorWebApp.Models.Transaction.Get
{
    public class TransactionListVM
    {
        //public int PaymentId { get; set; }

        public DateTime PaymentDate { get; set; }

        public bool PaymentStatus { get; set; }

        public string PaymentType { get; set; } = null!;

        public DateTime ConfirmDate { get; set; }
    }
}
