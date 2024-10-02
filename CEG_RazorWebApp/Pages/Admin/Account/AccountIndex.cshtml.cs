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

        private readonly ChildrenEnglishGameLibrary methcall = new();

        public string? LayoutUrl { get; set; } = Constants.ADMIN_LAYOUT_URL;

        public List<AccountStatusVM>? AccountStatuses { get; set; }
        public CreateTeacherVM? CreateTeacher { get; set; }
        public CreateParentVM? CreateParent { get; set; }
        public CreateStudentVM? CreateStudent { get; set; }

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
            methcall.InitTempData(this);
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

                return RedirectToPage("/Admin/Index");
            }
            if (!accountListResponse.Status)
            {
                _logger.LogError("Error while getting account list");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while getting account list !";

                return RedirectToPage("/Admin/Index");
            }
            /*TempData[Constants.ALERT_DEFAULT_SUCCESS_NAME] = ViewBag.Success = "Account List Get Successfully!";*/
            if(!TempData.ContainsKey(Constants.ALERT_DEFAULT_ERROR_NAME) || !TempData.ContainsKey(Constants.ALERT_DEFAULT_SUCCESS_NAME))
            {
                TempData[Constants.ALERT_DEFAULT_SUCCESS_NAME] = "Account List Get Successfully!";
            }
            var teacherTempData = methcall.GetValidationTempData<CreateTeacherVM>(this, TempData, Constants.CREATE_TEACHER_DETAILS_VALID, "createTeacher", jsonOptions);
            var parentTempData = methcall.GetValidationTempData<CreateParentVM>(this, TempData, Constants.CREATE_PARENT_DETAILS_VALID, "createParent", jsonOptions);
            var studentTempData = methcall.GetValidationTempData<CreateStudentVM>(this, TempData, Constants.CREATE_STUDENT_DETAILS_VALID, "createStudent", jsonOptions);

            AccountStatuses = _mapper.Map<List<AccountStatusVM>>(accountListResponse.Data);
            CreateTeacher = teacherTempData ?? new CreateTeacherVM();
            CreateParent = parentTempData ?? new CreateParentVM();
            CreateStudent = studentTempData ?? new CreateStudentVM();
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
        public async Task<IActionResult> OnPostTeacherAsync(
            [FromForm][Required] CreateTeacherVM? createTeacher)
        {
            AdminAPI_URL += "Admin/Teacher/Create";
            string? accToken = HttpContext.Session.GetString(Constants.ACC_TOKEN);
            /*await TryUpdateModelAsync(createTeacher, nameof(CreateTeacher));
            if (!TryValidateModel(CreateTeacher))
            {
                _logger.LogError("Validation Error Count: {}", ModelState.ErrorCount);
                TempData = methcall.SetValidationTempData(TempData, Constants.CREATE_TEACHER_DETAILS_VALID, CreateTeacher, jsonOptions);
                return Redirect("/Admin/Account/Index");
            }*/
            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.CREATE_TEACHER_DETAILS_VALID, createTeacher, jsonOptions);
                return Redirect("/Admin/Account/Index");
            }

            var teacherAccountCreateResponse = await methcall.CallMethodReturnObject<AdminAccountCreateResponseVM>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.POST_METHOD,
                url: AdminAPI_URL,
                inputType: _mapper.Map<CreateNewTeacher>(createTeacher),
                accessToken: accToken,
                _logger: _logger);

            if (teacherAccountCreateResponse == null)
            {
                _logger.LogError("Error while registering Teacher account");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while registering Teacher account !";

                return Redirect("/Admin/Account/Index");
            }
            if (!teacherAccountCreateResponse.Status)
            {
                _logger.LogError("Error while registering Teacher account");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while registering Teacher account !";

                return Redirect("/Admin/Account/Index");
            }
            TempData[Constants.ALERT_DEFAULT_SUCCESS_NAME] = "Teacher Account Create Successfully!";

            return Redirect("/Admin/Account/Index");
        }
        public async Task<IActionResult> OnPostParentAsync(
            [FromForm][Required] CreateParentVM? createParent)
        {
            AdminAPI_URL += "Admin/Parent/Create";
            string? accToken = HttpContext.Session.GetString(Constants.ACC_TOKEN);
            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.CREATE_PARENT_DETAILS_VALID, createParent, jsonOptions);
                return Redirect("/Admin/Account/Index");
            }

            var teacherAccountCreateResponse = await methcall.CallMethodReturnObject<AdminAccountCreateResponseVM>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.POST_METHOD,
                url: AdminAPI_URL,
                inputType: _mapper.Map<CreateNewParent>(createParent),
                accessToken: accToken,
                _logger: _logger);

            if (teacherAccountCreateResponse == null)
            {
                _logger.LogError("Error while registering Parent account");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while registering Parent account !";

                return Redirect("/Admin/Account/Index");
            }
            if (!teacherAccountCreateResponse.Status)
            {
                _logger.LogError("Error while registering Parent account");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while registering Parent account !";

                return Redirect("/Admin/Account/Index");
            }
            TempData[Constants.ALERT_DEFAULT_SUCCESS_NAME] = "Parent Account Create Successfully!";

            return Redirect("/Admin/Account/Index");
        }
        public async Task<IActionResult> OnPostStudentAsync(
            [FromForm][Required] CreateStudentVM? createStudent)
        {
            AdminAPI_URL += "Admin/Student/Create";
            string? accToken = HttpContext.Session.GetString(Constants.ACC_TOKEN);
            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.CREATE_STUDENT_DETAILS_VALID, createStudent, jsonOptions);
                return Redirect("/Admin/Account/Index");
            }

            var teacherAccountCreateResponse = await methcall.CallMethodReturnObject<AdminAccountCreateResponseVM>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.POST_METHOD,
                url: AdminAPI_URL,
                inputType: _mapper.Map<CreateNewStudent>(createStudent),
                accessToken: accToken,
                _logger: _logger);

            if (teacherAccountCreateResponse == null)
            {
                _logger.LogError("Error while registering Student account");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while registering Student account !";

                return Redirect("/Admin/Account/Index");
            }
            if (!teacherAccountCreateResponse.Status)
            {
                _logger.LogError("Error while registering Student account");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while registering Student account !";

                return Redirect("/Admin/Account/Index");
            }
            TempData[Constants.ALERT_DEFAULT_SUCCESS_NAME] = "Student Account Create Successfully!";

            return Redirect("/Admin/Account/Index");
        }
    }
}
