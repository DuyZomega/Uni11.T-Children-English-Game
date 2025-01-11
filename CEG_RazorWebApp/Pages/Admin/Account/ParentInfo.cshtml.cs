using CEG_RazorWebApp.Libraries;
using CEG_RazorWebApp.Models.Account.Get;
using CEG_RazorWebApp.Models.Account.Update;
using CEG_RazorWebApp.Models.Parent.Update;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CEG_RazorWebApp.Pages.Admin.Account
{
    public class ParentInfoModel : PageModel
    {
        public string? LayoutUrl { get; set; } = Constants.ADMIN_LAYOUT_URL;
        public AccountInfoVM? AccountInfo { get; set; } = new AccountInfoVM();
        public UpdateParentVM? UpdateParentInfo { get; set; } = new UpdateParentVM();
        public UpdatePasswordVM? UpdatePasswordInfo { get; set; } = new UpdatePasswordVM();
        public int AccountId = 0;
        public void OnGet(
            [FromRoute][Required] int accountId)
        {
            AccountId = accountId;
        }
    }
}
