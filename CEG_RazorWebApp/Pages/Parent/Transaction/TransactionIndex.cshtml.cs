using AutoMapper;
using CEG_RazorWebApp.Libraries;
using CEG_RazorWebApp.Models.Transaction.Create;
using CEG_RazorWebApp.Models.Transaction.Get;
using CEG_RazorWebApp.Pages.Admin.Account;
using CEG_RazorWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace CEG_RazorWebApp.Pages.Parent.Transaction
{
    public class TransactionIndexModel : PageModel
    {
        private IConfiguration _config;
        private readonly IVnPayService _vnPayService;
        public string? LayoutUrl { get; set; } = Constants.PARENT_LAYOUT_URL;
        public CreateTransactionVM CreateTransactionInfo { get; set; } = new CreateTransactionVM();

        public TransactionIndexModel(IConfiguration config, IVnPayService vnPayService)
        {
            _config = config;
            _vnPayService = vnPayService;
        }

        public void OnGet()
        {

        }
    }
}
