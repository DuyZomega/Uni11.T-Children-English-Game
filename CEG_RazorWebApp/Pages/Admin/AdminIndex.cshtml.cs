using AutoMapper;
using CEG_RazorWebApp.Libraries;
using CEG_RazorWebApp.Pages.Admin.Course;
using CEG_RazorWebApp.Pages.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

namespace CEG_RazorWebApp.Pages.Admin
{
    [Authorize(Policy = "SessionAuthorize")]
    public class AdminIndexModel : PageModel
    {
		private readonly ILogger<AdminIndexModel> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
		private readonly HttpClient _httpClient = null;
		//private readonly IVnPayService _vnPayService;
		private string AdminAPI_URL = "";
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
        public string? LayoutUrl { get; set; } = Constants.ADMIN_LAYOUT_URL;

        public AdminIndexModel(ILogger<AdminIndexModel> logger, IConfiguration config, IMapper mapper)
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

        public void OnGet()
        {
            methcall.InitTempData(this);
        }
        public IActionResult OnGetLogout()
        {
            _httpClient.DefaultRequestHeaders.Authorization = null;
            HttpContext.Session.Clear();
            TempData.Clear();
            SignOut();

            return RedirectToPage(Constants.LOGOUT_REDIRECT_URL);
        }
    }
}
