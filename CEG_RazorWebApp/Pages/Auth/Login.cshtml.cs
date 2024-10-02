using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json;
using CEG_RazorWebApp.Libraries;
using System.Net.Http.Headers;
using Azure.Storage.Blobs.Models;
using CEG_BAL.ViewModels.Authenticates;
using CEG_RazorWebApp.Models.Account.Response;
using Microsoft.AspNetCore.Authorization;

namespace CEG_RazorWebApp.Pages.Auth
{
	[AllowAnonymous]
    public class LoginModel : PageModel
    {
		[BindProperty]
        public AuthenRequest AuthenRequest { get; set; }

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

		/*public IActionResult OnGetLogin()
        {
			string? role = HttpContext.Session.GetString(Constants.ROLE_NAME);

			if (role == null) role = "Guest";

			TempData[Constants.ROLE_NAME] = role;

			return Page();
		}*/

		public IActionResult OnGetLogout()
		{
			client.DefaultRequestHeaders.Authorization = null;
			HttpContext.Session.Clear();
			TempData[Constants.ACC_TOKEN] = null;
			TempData[Constants.ROLE_NAME] = null;
			TempData[Constants.USR_ID] = null;
			SignOut();

			// If using ASP.NET Identity, you may want to sign out the user
			// Example: await SignInManager.SignOutAsync();

			return RedirectToPage(Constants.LOGOUT_REDIRECT_URL);
		}

		public void OnGet()
		{

		}

		public async Task<IActionResult> OnPostAsync()
		{
			AuthenAPI_URL += "Account/Login";

			var authenResponse = await methcall.CallMethodReturnObject<AuthenResponseVM>(
				_httpClient: client,
				options: jsonOptions,
				methodName: Constants.POST_METHOD,
				url: AuthenAPI_URL,
				inputType: AuthenRequest,
				_logger: _logger);

			if (authenResponse == null || authenResponse.Data == null || !authenResponse.Status)
			{
				string? role = HttpContext.Session.GetString(Constants.ROLE_NAME);

				if (role == null) role = Constants.GUEST;

				TempData[Constants.ROLE_NAME] = role;
				_logger.LogInformation("Username or Password is invalid.");
                //ViewBag.error = "Username or Password is invalid.";
                return Page();
            }
			var responseAuth = authenResponse.Data;

			if (authenResponse.Status)
			{
				HttpContext.Session.SetString(Constants.ACC_TOKEN, responseAuth.AccessToken);
				HttpContext.Session.SetString(Constants.ROLE_NAME, responseAuth.RoleName);
				HttpContext.Session.SetString(Constants.USR_ID, responseAuth.AccountId);
				HttpContext.Session.SetString(Constants.USR_NAME, responseAuth.UserName);
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", responseAuth.AccessToken);

				TempData[Constants.ACC_TOKEN] = responseAuth.AccessToken;
				TempData[Constants.ROLE_NAME] = responseAuth.RoleName;
				TempData[Constants.USR_ID] = responseAuth.AccountId;
				TempData[Constants.USR_NAME] = responseAuth.UserName;
			}
			switch (responseAuth.RoleName)
			{
				case var value when value.Equals(Constants.ADMIN):
					{
						_logger.LogInformation("Admin Login Successful: " + TempData[Constants.ROLE_NAME] + " , Id: " + TempData[Constants.USR_ID]);
						return Redirect(Constants.ADMIN_URL);
					}
				case var value when value.Equals(Constants.TEACHER):
					{
						_logger.LogInformation("Teacher Login Successful: " + TempData[Constants.ROLE_NAME] + " , Id: " + TempData[Constants.USR_ID]);
						return RedirectToPage(Constants.TEACHER_URL);
					}
				case var value when value.Equals(Constants.PARENT):
					{
						_logger.LogInformation("Parent Login Successful: " + TempData[Constants.ROLE_NAME] + " , Id: " + TempData[Constants.USR_ID]);
						return RedirectToPage(Constants.PARENT_URL);
					}
				case var value when value.Equals(Constants.STUDENT):
					{
						_logger.LogInformation("Student Login Successful: " + TempData[Constants.ROLE_NAME] + " , Id: " + TempData[Constants.USR_ID]);
						return RedirectToPage(Constants.STUDENT_URL);
					}
			}
			_logger.LogInformation("Goofy Ahh Member Login Successful: " + TempData[Constants.ROLE_NAME] + " , Id: " + TempData[Constants.USR_ID]);
			return Page();
		}
    }
}
