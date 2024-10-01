using AutoMapper;
using CEG_RazorWebApp.Libraries;
using CEG_RazorWebApp.Models.Admin.Response;
using CEG_RazorWebApp.Models.Class.Get;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace CEG_RazorWebApp.Pages.Admin.Class
{
    public class ClassIndexModel : PageModel
    {
        private readonly ILogger<ClassIndexModel> _logger;
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
        private readonly ChildrenEnglishGameLibrary methcall = new();
        [BindProperty]
        public List<IndexClassInfoVM>? Classes { get; set; }

        public ClassIndexModel(ILogger<ClassIndexModel> logger, IConfiguration config, IMapper mapper)
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
        public async Task<IActionResult> OnGetAsync()
        {
            methcall.InitTempData(this);
            AdminAPI_URL += "Class/All";
            string? accToken = HttpContext.Session.GetString(Constants.ACC_TOKEN);

            var courseListResponse = await methcall.CallMethodReturnObject<AdminClassListResponseVM>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.GET_METHOD,
                url: AdminAPI_URL,
                accessToken: accToken,
                _logger: _logger);

            if (courseListResponse == null)
            {
                _logger.LogError("Error while getting class list");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while getting class list !";

                return RedirectToAction("AdminIndex");
            }
            if (!courseListResponse.Status)
            {
                _logger.LogError("Error while getting class list");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while getting class list !";

                return RedirectToAction("AdminIndex");
            }
            //TempData[Constants.ALERT_DEFAULT_SUCCESS_NAME] = ViewBag.Success = "Class List Get Successfully!";
            TempData[Constants.ALERT_DEFAULT_SUCCESS_NAME] = "Class List Get Successfully!";
            //var teacherTempData = methcall.GetValidationTempData<CreateTeacherVM>(this, TempData, Constants.CREATE_TEACHER_DETAILS_VALID, "createTeacher", jsonOptions);

            Classes = _mapper.Map<List<IndexClassInfoVM>>(courseListResponse.Data);
            //CreateClass = new CreateClassVM()

            return Page();
        }
        public IActionResult OnGetLogout()
        {
            _httpClient.DefaultRequestHeaders.Authorization = null;
            HttpContext.Session.Clear();
            TempData[Constants.ACC_TOKEN] = null;
            TempData[Constants.ROLE_NAME] = null;
            TempData[Constants.USR_ID] = null;
            SignOut();

            // If using ASP.NET Identity, you may want to sign out the user
            // Example: await SignInManager.SignOutAsync();

            return RedirectToPage("/Home/Index");
        }
    }
}
