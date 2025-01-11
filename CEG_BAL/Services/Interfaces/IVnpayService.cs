using CEG_BAL.ViewModels.Transaction;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Interfaces
{
    public interface IVnpayService
    {
        string CreatePaymentUrl(TransactionRequest request);
        TransactionResponse PaymentExecute();
    }
}
