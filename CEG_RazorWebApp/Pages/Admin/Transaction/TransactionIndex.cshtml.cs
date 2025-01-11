using AutoMapper;
using CEG_RazorWebApp.Libraries;
using CEG_RazorWebApp.Models.Transaction.Create;
using CEG_RazorWebApp.Models.VnPay;
using CEG_RazorWebApp.Pages.Admin.Course;
using CEG_RazorWebApp.Services.Implements;
using CEG_RazorWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace CEG_RazorWebApp.Pages.Admin.Transaction
{
    public class TransactionIndexModel : PageModel
    {
        private IConfiguration _config;
        private readonly IVnPayService _vnPayService;
        public string? LayoutUrl { get; set; } = Constants.ADMIN_LAYOUT_URL;
        public CreateTransactionVM CreateTransactionInfo { get; set; } = new CreateTransactionVM();

        public TransactionIndexModel(IConfiguration config, IVnPayService vnPayService)
        {
            _config = config;
            _vnPayService = vnPayService;
        }

        public void OnGet()
        {

        }
        /*public IActionResult OnPostGeneratePaymentUrl([FromBody] CreateTransactionVM createTransaction)
        public IActionResult OnPostGeneratePaymentUrl(
            [FromBody] CreateTransactionVM createTransaction
            )
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new { x.Key, x.Value.Errors })
                    .ToList();

                return new JsonResult(new { status = false, message = "Model validation failed", errors });
            }

            var paymentUrl = _vnPayService.CreatePaymentUrl(createTransaction, HttpContext);
            return new JsonResult(new { status = true, paymentUrl = paymentUrl });
        }*/
    }
}
