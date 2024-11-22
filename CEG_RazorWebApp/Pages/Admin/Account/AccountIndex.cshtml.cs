using AutoMapper;
using CEG_BAL.ViewModels.Account.Create;
using CEG_RazorWebApp.Libraries;
using CEG_RazorWebApp.Models.Account.Create;
using CEG_RazorWebApp.Models.Account.Get;
using CEG_RazorWebApp.Models.Admin.Response;
using CEG_RazorWebApp.Pages.Admin.Course;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace CEG_RazorWebApp.Pages.Admin.Account
{
    public class AccountIndexModel : PageModel
    {
        private readonly ILogger<AccountIndexModel> _logger;
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

        private readonly CEG_RAZOR_Library methcall = new();

        public string? LayoutUrl { get; set; } = Constants.ADMIN_LAYOUT_URL;

        public List<AccountStatusVM>? AccountStatuses { get; set; } = new List<AccountStatusVM>();
        public CreateTeacherVM? CreateTeacher { get; set; } = new CreateTeacherVM();
        public CreateParentVM? CreateParent { get; set; } = new CreateParentVM();
        public CreateStudentVM? CreateStudent { get; set; } = new CreateStudentVM();

        public AccountIndexModel(ILogger<AccountIndexModel> logger, IConfiguration config, IMapper mapper)
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
        }
    }
}
