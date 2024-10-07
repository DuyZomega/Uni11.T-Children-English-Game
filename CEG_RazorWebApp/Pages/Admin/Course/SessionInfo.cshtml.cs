using AutoMapper;
using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Admin;
using CEG_RazorWebApp.Libraries;
using CEG_RazorWebApp.Models.Admin.Response;
using CEG_RazorWebApp.Models.Homework.Create;
using CEG_RazorWebApp.Models.Homework.Get;
using CEG_RazorWebApp.Models.Homework.Update;
using CEG_RazorWebApp.Models.Session.Get;
using CEG_RazorWebApp.Models.Session.Update;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace CEG_RazorWebApp.Pages.Admin.Course
{
    public class SessionInfoModel : PageModel
    {
        private readonly ILogger<SessionInfoModel> _logger;
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
        [BindProperty]
        public int? CourseId { get; set; }
        public SessionInfoVM? SessionInfo { get; set; }
        public UpdateSessionVM? UpdateSessionInfo { get; set; }
        public CreateHomeworkVM? CreateHomework { get; set; }
        public List<AdminHomeworkInfoPVM>? Homeworks { get; set; }
        public SessionInfoModel(ILogger<SessionInfoModel> logger, IConfiguration config, IMapper mapper)
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
            [FromRoute][Required] int courseId,
            [FromRoute][Required] int sessionId)
        {
            methcall.InitTempData(this);
            CourseId = courseId;
            AdminAPI_URL += "Session/" + sessionId;
            string? accToken = HttpContext.Session.GetString(Constants.ACC_TOKEN);

            var sessionInfoResponse = await methcall.CallMethodReturnObject<AdminSessionInfoResponseVM>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.GET_METHOD,
                url: AdminAPI_URL,
                accessToken: accToken,
                _logger: _logger);

            if (sessionInfoResponse == null)
            {
                _logger.LogError("Error while getting session info");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while getting session info !";

                return Redirect("/Admin/Course/" + CourseId + "/Info");
            }
            if (!sessionInfoResponse.Status || sessionInfoResponse.Data == null)
            {
                _logger.LogError("Error while getting course info");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while getting session info !";

                return Redirect("/Admin/Course/" + CourseId + "/Info");
            }
            TempData[Constants.ALERT_DEFAULT_SUCCESS_NAME] = "Session Info Get Successfully!";

            var createHomeworkFailed = methcall.GetValidationTempData<CreateHomeworkVM>(this, TempData, Constants.CREATE_HOMEWORK_DETAILS_VALID, "createHomework", jsonOptions);
            var updateSessionFailed = methcall.GetValidationTempData<UpdateSessionVM>(this, TempData, Constants.UPDATE_SESSION_DETAILS_VALID, "updateSession", jsonOptions);
            var updateHomeworkFailed = methcall.GetValidationTempData<UpdateHomeworkVM>(this, TempData, Constants.UPDATE_HOMEWORK_DETAILS_VALID, "updateHomework", jsonOptions);
            var homeworkList = new List<AdminHomeworkInfoPVM>();

            if (sessionInfoResponse.Data.Homeworks != null && sessionInfoResponse.Data.Homeworks.Count > 0)
                foreach (var homework in sessionInfoResponse.Data.Homeworks)
                {
                    homeworkList.Add(new AdminHomeworkInfoPVM(
                        CourseId,
                        sessionId,
                        _mapper.Map<HomeworkInfoVM>(homework),
                        updateHomeworkFailed != null && updateHomeworkFailed.HomeworkId.Equals(homework.HomeworkId) ? updateHomeworkFailed : _mapper.Map<UpdateHomeworkVM>(homework)
                        )
                    );
                }
            SessionInfo = _mapper.Map<SessionInfoVM>(sessionInfoResponse.Data);
            UpdateSessionInfo = updateSessionFailed ?? _mapper.Map<UpdateSessionVM>(sessionInfoResponse.Data);
            Homeworks = homeworkList ?? new List<AdminHomeworkInfoPVM>();
            CreateHomework = createHomeworkFailed ?? new CreateHomeworkVM();
            return Page();
        }
        public async Task<IActionResult> OnPostUpdate(
            [FromRoute][Required] int sessionId,
            [FromForm][Required] UpdateSessionVM updateSession)
        {
            AdminAPI_URL += "Session/" + sessionId + "/Update";
            string? accToken = HttpContext.Session.GetString(Constants.ACC_TOKEN);
            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.UPDATE_SESSION_DETAILS_VALID, updateSession, jsonOptions);
                return Redirect("/Admin/Course/" + CourseId + "/Session/"+ sessionId +"/Info");
            }
            var courseInfoResponse = await methcall.CallMethodReturnObject<AdminSessionUpdateResponseVM>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.PUT_METHOD,
                url: AdminAPI_URL,
                accessToken: accToken,
                inputType: _mapper.Map<SessionViewModel>(updateSession),
                _logger: _logger);

            if (courseInfoResponse == null)
            {
                _logger.LogError("Error while getting session info");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while getting session info !";

                return Redirect("/Admin/Course/" + CourseId + "/Session/" + sessionId + "/Info");
            }
            if (!courseInfoResponse.Status)
            {
                _logger.LogError("Error while getting session info");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while getting session info !";

                return Redirect("/Admin/Course/" + CourseId + "/Session/" + sessionId + "/Info");
            }
            TempData[Constants.ALERT_DEFAULT_SUCCESS_NAME] = "Session Info Update Successfully!";

            return Redirect("/Admin/Course/" + CourseId + "/Session/" + sessionId + "/Info");
        }
        public async Task<IActionResult> OnPostCreate(
            [FromRoute][Required] int sessionId,
            [FromForm][Required] CreateHomeworkVM createHomework)
        {
            AdminAPI_URL += "Homework/Create";
            string? accToken = HttpContext.Session.GetString(Constants.ACC_TOKEN);

            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.CREATE_HOMEWORK_DETAILS_VALID, createHomework, jsonOptions);
                return Redirect("/Admin/Course/" + CourseId + "/Session/" + sessionId + "/Info");
            }

            var authenResponse = await methcall.CallMethodReturnObject<AdminHomeworkCreateResponseVM>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.POST_METHOD,
                url: AdminAPI_URL,
                inputType: _mapper.Map<CreateNewHomework>(createHomework),
                accessToken: accToken,
                _logger: _logger);

            if (authenResponse == null)
            {
                _logger.LogError("Error while registering Homework");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while registering Homework !";

                return Redirect("/Admin/Course/" + CourseId + "/Session/" + sessionId + "/Info");
            }
            if (!authenResponse.Status)
            {
                _logger.LogError("Error while registering Homework");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while registering Homework !";

                return Redirect("/Admin/Course/" + CourseId + "/Session/" + sessionId + "/Info");
            }
            TempData[Constants.ALERT_DEFAULT_SUCCESS_NAME] = "Homework Create Successfully!";
            return Redirect("/Admin/Course/" + CourseId + "/Session/" + sessionId + "/Info");
        }
        public async Task<IActionResult> OnPostHomeworkUpdate(
            [FromRoute][Required] int sessionId,
            [Required] int homeworkId,
            [FromForm][Required] UpdateHomeworkVM updateHomework)
        {
            AdminAPI_URL += "Homework/" + homeworkId + "/Update";
            string? accToken = HttpContext.Session.GetString(Constants.ACC_TOKEN);
            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.UPDATE_HOMEWORK_DETAILS_VALID, updateHomework, jsonOptions);
                return Redirect("/Admin/Course/" + CourseId + "/Session/" + sessionId + "/Info");
            }
            var courseInfoResponse = await methcall.CallMethodReturnObject<AdminHomeworkUpdateResponseVM>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.PUT_METHOD,
                url: AdminAPI_URL,
                accessToken: accToken,
                inputType: _mapper.Map<HomeworkViewModel>(updateHomework),
                _logger: _logger);

            if (courseInfoResponse == null)
            {
                _logger.LogError("Error while updating homework info");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while updating homework info !";

                return Redirect("/Admin/Course/" + CourseId + "/Session/" + sessionId + "/Info");
            }
            if (!courseInfoResponse.Status)
            {
                _logger.LogError("Error while updating homework info");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while updating homework info !";

                return Redirect("/Admin/Course/" + CourseId + "/Session/" + sessionId + "/Info");
            }
            TempData[Constants.ALERT_DEFAULT_SUCCESS_NAME] = "Homework Info Update Successfully!";

            return Redirect("/Admin/Course/" + CourseId + "/Session/" + sessionId + "/Info");
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
