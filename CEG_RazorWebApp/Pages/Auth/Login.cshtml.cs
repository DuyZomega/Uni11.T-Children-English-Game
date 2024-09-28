using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json;
using CEG_RazorWebApp.Libraries;
using System.Net.Http.Headers;

namespace CEG_RazorWebApp.Pages.Auth
{
    public class LoginModel : PageModel
    {
		private readonly ILogger<LoginModel> _logger;
		private readonly IConfiguration _config;
		private readonly HttpClient client = null;
		//private readonly IVnPayService _vnPayService;
		private string AuthenAPI_URL = "";
		private ChildrenEnglishGameLibrary methcall = new();
		private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true,
		};
		private readonly CookieOptions cookieOptions = new CookieOptions
		{
			Expires = DateTime.Now.AddMinutes(10),
			MaxAge = TimeSpan.FromMinutes(10),
			Secure = true,
			IsEssential = true,
		};

		public LoginModel(ILogger<LoginModel> logger, IConfiguration config)
		{
			_logger = logger;
			_config = config;
			client = new HttpClient()
			{
				BaseAddress = new Uri(config.GetSection(Constants.SYSTEM_DEFAULT_API_HTTPS_URL_CONFIG_PATH).Value)
			};
			//_vnPayService = vnPayService;
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			AuthenAPI_URL = config.GetSection(Constants.SYSTEM_DEFAULT_API_URL_CONFIG_PATH).Value;
		}

		public IActionResult OnGetLogin()
        {
			string? role = HttpContext.Session.GetString(Constants.ROLE_NAME);

			if (role == null) role = "Guest";

			TempData[Constants.ROLE_NAME] = role;

			return Page();
		}
    }
}
