using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels.Transaction
{
    public class CreateTransaction
    {
        public string ParentFullname { get; set; } = null!;
        public string VnpayId { get; set; } = null!;
        public int TransactionAmount { get; set; }
        public string TransactionType { get; set; } = null!;
        public string? StudentFullname { get; set; }
        public string? ClassName { get; set; }
    }
}
