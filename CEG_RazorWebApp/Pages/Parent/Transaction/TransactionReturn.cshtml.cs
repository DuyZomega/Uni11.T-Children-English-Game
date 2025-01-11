using CEG_BAL.ViewModels;
using CEG_RazorWebApp.Libraries;
using CEG_RazorWebApp.Models.Transaction.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CEG_RazorWebApp.Pages.Parent.Transaction
{
    public class TransactionReturnModel : PageModel
    {
        public TransactionResponseVM TransactionResponse { get; set; } = new TransactionResponseVM();
        private readonly IConfiguration _config;
        private readonly VnPayLibrary _vnPayLibrary;
        private readonly HttpClient _httpClient;
        public TransactionReturnModel(IConfiguration config, HttpClient httpClient)
        {
            _config = config;
            _vnPayLibrary = new VnPayLibrary();
            _httpClient = httpClient;
        }
        [BindProperty]
        public TransactionViewModel CreateTransactionVM { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            string hashSecret = _config["Vnpay:HashSecret"]; // Replace with actual VNPay hash secret
            TransactionResponse = _vnPayLibrary.GetFullResponseData(Request.Query, hashSecret);

            if (TransactionResponse.Success)
            {
                TransactionResponse.Message = "Transaction Successful!";
                string orderDetails = TransactionResponse.OrderDescription;
                // Call the API to create the transaction in the database
                var transactionData = new TransactionViewModel
                {
                    ParentFullname = ExtractValue(orderDetails, Constants.VNPAY_PARENT_NAME_LABEL),
                    TransactionAmount = (int)TransactionResponse.Value / 100,
                    TransactionDate = DateTime.Now,
                    TransactionStatus = "Completed",
                    ConfirmDate = DateTime.Now,
                    TransactionType = TransactionResponse.TransactionType,
                    VnpayId = TransactionResponse.VnpayId
                };

                if (transactionData.TransactionType == "Enrollment")
                {
                    transactionData.StudentFullname = ExtractValue(orderDetails, Constants.VNPAY_STUDENT_NAME_LABEL);
                    transactionData.ClassName = ExtractValue(orderDetails, Constants.VNPAY_CLASS_NAME_LABEL);
                }

                CreateTransactionVM = transactionData;
            }
            else
            {
                TransactionResponse.Message = "Transaction Failed.";
            }
            return Page();
        }

        // Helper function for extracting values based on labels
        private string ExtractValue(string details, string label)
        {
            int startIndex = details.IndexOf(label);
            if (startIndex == -1) return string.Empty; // Label not found, return empty string
            startIndex += label.Length; // Move to the end of the label
            int endIndex = details.IndexOf(',', startIndex); // Find the next comma
            if (endIndex == -1) endIndex = details.Length; // If no comma, take until the end
            return details.Substring(startIndex, endIndex - startIndex).Trim();
        }
    }
}
