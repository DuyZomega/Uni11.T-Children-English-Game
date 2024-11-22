using CEG_RazorWebApp.Models.Transaction.Create;
using CEG_RazorWebApp.Models.Transaction.Response;
using CEG_RazorWebApp.Models.VnPay;

namespace CEG_RazorWebApp.Services.Interfaces
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(CreateTransactionVM model, HttpContext context);
        TransactionResponseVM PaymentExecute(IQueryCollection collections);
    }
}
