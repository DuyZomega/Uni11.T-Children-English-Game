using BAL.ViewModels;
using CEG_WebMVC.Models;

namespace CEG_WebMVC.Models.Transaction
{
    public class GetUserPaymentResponse : DefaultResponseViewModel<IEnumerable<TransactionViewModel>>
    {
        public GetUserPaymentResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
