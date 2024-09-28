using AutoMapper;
using CEG_RazorWebApp.Libraries;
using CEG_RazorWebApp.Models.Account.Create;
using CEG_RazorWebApp.Models.Account.Get;
using CEG_RazorWebApp.Models.Admin.Response;
using CEG_RazorWebApp.Pages.Admin.Course;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        private readonly ChildrenEnglishGameLibrary methcall = new();
        [BindProperty]
        public List<AccountStatusVM> AccountStatuses { get; set; }
        [BindProperty]
        public CreateTeacherVM CreateTeacher { get; set; }
        [BindProperty]
        public CreateParentVM CreateParent { get; set; }
        [BindProperty]
        public CreateStudentVM CreateStudent { get; set; }
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
        public async Task<IActionResult> OnGetAsync()
        {
            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.ADMIN) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.ADMIN));
            AdminAPI_URL += "Account/All";
            string? accToken = HttpContext.Session.GetString(Constants.ACC_TOKEN);

            var accountListResponse = await methcall.CallMethodReturnObject<AdminAccountListResponseVM>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.GET_METHOD,
                url: AdminAPI_URL,
                accessToken: accToken,
                _logger: _logger);

            if (accountListResponse == null)
            {
                _logger.LogError("Error while getting account list");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while getting account list !";

                return RedirectToAction("AdminIndex");
            }
            if (!accountListResponse.Status)
            {
                _logger.LogError("Error while getting account list");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while getting account list !";

                return RedirectToAction("AdminIndex");
            }
            /*TempData[Constants.ALERT_DEFAULT_SUCCESS_NAME] = ViewBag.Success = "Account List Get Successfully!";*/
            TempData[Constants.ALERT_DEFAULT_SUCCESS_NAME] = "Account List Get Successfully!";
            var teacherTempData = methcall.GetValidationTempData<CreateTeacherVM>(this, TempData, Constants.CREATE_TEACHER_DETAILS_VALID, "createTeacher", jsonOptions);

            AccountStatuses = _mapper.Map<List<AccountStatusVM>>(accountListResponse.Data);
            CreateTeacher = teacherTempData != null ? teacherTempData : new CreateTeacherVM();

            return Page();
        }
    }
}
