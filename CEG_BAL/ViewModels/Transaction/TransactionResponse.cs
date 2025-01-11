using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.ViewModels.Transaction
{
    public class TransactionResponse
    {
        public long TransactionId { get; set; }
        public string OrderDescription { get; set; }
        public string TransactionType { get; set; }
        public decimal Value { get; set; }
        public string VnpayId { get; set; }
        public string PaymentMethod { get; set; }
        public bool Success { get; set; }
        public string Token { get; set; }
        public string VnPayResponseCode { get; set; }
        public string Message { get; set; }
    }
}
