namespace CEG_RazorWebApp.Models.Transaction.Get
{
    public class TransactionListVM
    {
        public int TransactionId { get; set; }

        public string VnpayId { get; set; } = null!;

        public int TransactionAmount { get; set; }

        public DateTime TransactionDate { get; set; }

        public bool TransactionStatus { get; set; }

        public string TransactionType { get; set; } = null!;

        public DateTime ConfirmDate { get; set; }
    }
}
