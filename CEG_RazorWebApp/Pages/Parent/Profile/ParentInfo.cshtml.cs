using CEG_RazorWebApp.Libraries;
using CEG_RazorWebApp.Models.Account.Get;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CEG_RazorWebApp.Pages.Parent.Profile
{
    public class ParentInfoModel : PageModel
    {
        public string? LayoutUrl { get; set; } = Constants.PARENT_LAYOUT_URL;
        public AccountInfoVM AccountInfo { get; set; } = new AccountInfoVM();
        public void OnGet()
        {
        }
    }
}
