using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels.Teacher.Transaction
{
    public class EarningViewModel
    {
        public int TransactionId { get; set; }

        public int AccountId { get; set; }

        public string? PayerFullname { get; set; }

        public string? PaymentMethod { get; set; }

        public string? ReceiverFullname { get; set; }

        public string? ClassName { get; set; }

        public string? VnpayId { get; set; }

        public int TransactionAmount { get; set; }

        public DateTime TransactionDate { get; set; }

        public string TransactionStatus { get; set; } = null!;

        public string TransactionType { get; set; } = null!;

        public DateTime ConfirmDate { get; set; }

        public string? Description { get; set; }
    }
}
