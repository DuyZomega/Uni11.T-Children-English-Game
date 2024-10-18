using AutoMapper;
using CEG_RazorWebApp.Libraries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CEG_RazorWebApp.Pages.Teacher
{

        [Authorize(Policy = "SessionAuthorize")]
        public class TeacherIndexModel : PageModel
        {
            private readonly ILogger<TeacherIndexModel> _logger;
            private readonly IMapper _mapper;
            private readonly IConfiguration _config;
            private readonly HttpClient _httpClient = null;
            //private readonly IVnPayService _vnPayService;
            private string StudentAPI_URL = "";
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

            public TeacherIndexModel(ILogger<TeacherIndexModel> logger, IConfiguration config, IMapper mapper)
            {
                _logger = logger;
                _config = config;
                _mapper = mapper;
                _httpClient = new HttpClient()
                {
                    BaseAddress = new Uri(config.GetSection(Constants.SYSTEM_DEFAULT_API_HTTPS_URL_CONFIG_PATH).Value)
                };
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                StudentAPI_URL = config.GetSection(Constants.SYSTEM_DEFAULT_API_URL_CONFIG_PATH).Value;
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

                // If using ASP.NET Identity, you may want to sign out the user
                // Example: await SignInManager.SignOutAsync();

                return RedirectToPage("/Home/Index");
            }
        }
    }
