using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CEG_RazorWebApp.Pages.Auth
{
    public class LoginModel : PageModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [PasswordPropertyText]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public void OnGet()
        {
        }
    }
}
