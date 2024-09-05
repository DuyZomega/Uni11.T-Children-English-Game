using BAL.ViewModels;
using CEG_WebMVC.Models;

namespace CEG_WebMVC.Models.Transaction
{
    public class GetTransactionResponse : DefaultResponseViewModel<TransactionViewModel>
    {
        public GetTransactionResponse(bool status, string? errorMessage, string? successMessage) : base(status, errorMessage, successMessage)
        {
        }
    }
}
