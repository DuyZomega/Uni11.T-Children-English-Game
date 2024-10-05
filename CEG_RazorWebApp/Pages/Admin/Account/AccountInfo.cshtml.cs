using AutoMapper;
using CEG_RazorWebApp.Libraries;
using CEG_RazorWebApp.Models.Account.Get;
using CEG_RazorWebApp.Models.Admin.Response;
using CEG_RazorWebApp.Pages.Admin.Course;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace CEG_RazorWebApp.Pages.Admin.Account
{
    public class AccountInfoModel : PageModel
    {
        private readonly ILogger<AccountInfoModel> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient = null;
        private string AdminAPI_URL = "";
        private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            PropertyNameCaseInsensitive = true
        };
        private readonly CookieOptions cookieOptions = new CookieOptions
        {
            Expires = DateTime.Now.AddMinutes(10),
            MaxAge = TimeSpan.FromMinutes(10),
            Secure = true,
            IsEssential = true,
        };
        private ChildrenEnglishGameLibrary methcall = new();
        public string? LayoutUrl { get; set; } = Constants.ADMIN_LAYOUT_URL;
        public AccountInfoVM? AccountInfo { get; set; }

        public AccountInfoModel(ILogger<AccountInfoModel> logger, IConfiguration config, IMapper mapper)
        {
            _logger = logger;
            _config = config;
            _mapper = mapper;
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(config.GetSection(Constants.SYSTEM_DEFAULT_API_HTTPS_URL_CONFIG_PATH).Value)
            };
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            AdminAPI_URL = config.GetSection(Constants.SYSTEM_DEFAULT_API_URL_CONFIG_PATH).Value;
        }
        public async Task<IActionResult> OnGetAsync(
            [FromRoute][Required] int accountId)
        {
            methcall.InitTempData(this);
            AdminAPI_URL += "Account/" + accountId;
            string? accToken = HttpContext.Session.GetString(Constants.ACC_TOKEN);

            var accountInfoResponse = await methcall.CallMethodReturnObject<AdminAccountInfoResponseVM>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.GET_METHOD,
                url: AdminAPI_URL,
                accessToken: accToken,
                _logger: _logger);

            if (accountInfoResponse == null)
            {
                _logger.LogError("Error while getting account info");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while getting account info !";

                return Redirect("/Admin/Account/Index");
            }
            if (!accountInfoResponse.Status || accountInfoResponse.Data == null)
            {
                _logger.LogError("Error while getting account info");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while getting account info !";

                return Redirect("/Admin/Account/Index");
            }
            if (!TempData.ContainsKey(Constants.ALERT_DEFAULT_ERROR_NAME) || !TempData.ContainsKey(Constants.ALERT_DEFAULT_SUCCESS_NAME))
            {
                TempData[Constants.ALERT_DEFAULT_SUCCESS_NAME] = "Account Info Get Successfully!";
            }

            AccountInfo = _mapper.Map<AccountInfoVM>(accountInfoResponse.Data);
            return Page();
        }

        public IActionResult OnGetLogout()
        {
            _httpClient.DefaultRequestHeaders.Authorization = null;
            HttpContext.Session.Clear();
            TempData.Clear();
            SignOut();

            // If using ASP.NET Identity, you may want to sign out the user
            // Example: await SignInManager.SignOutAsync();

            return RedirectToPage(Constants.LOGOUT_REDIRECT_URL);
        }
    }
}
