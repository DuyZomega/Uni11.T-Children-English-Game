using CEG_RazorWebApp.Models.Transaction.Create;
using CEG_RazorWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CEG_RazorWebApp.Pages.Teacher.Earning
{
    public class EarningIndexModel : PageModel
    {
        private IConfiguration _config;
        private readonly IVnPayService _vnPayService;

        public EarningIndexModel(IConfiguration config, IVnPayService vnPayService)
        {
            _config = config;
            _vnPayService = vnPayService;
        }

        public void OnGet()
        {
        }
    }
}
