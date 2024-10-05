using AutoMapper;
using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Admin;
using CEG_DAL.Models;
using CEG_RazorWebApp.Libraries;
using CEG_RazorWebApp.Models.Admin.Response;
using CEG_RazorWebApp.Models.Course.Get;
using CEG_RazorWebApp.Models.Course.Update;
using CEG_RazorWebApp.Models.Session.Create;
using CEG_RazorWebApp.Models.Session.Get;
using CEG_RazorWebApp.Models.Session.Update;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace CEG_RazorWebApp.Pages.Admin.Course
{
    public class CourseInfoModel : PageModel
    {
        private readonly ILogger<CourseInfoModel> _logger;
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
        public CourseInfoVM? CourseInfo { get; set; }
        public UpdateCourseVM? UpdateCourseInfo { get; set; }
        public CreateSessionVM? CreateSession { get; set; }
        public List<AdminSessionInfoPVM>? Sessions { get; set; }

        public CourseInfoModel(ILogger<CourseInfoModel> logger, IConfiguration config, IMapper mapper)
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
            [FromRoute][Required] int courseId)
        {
            methcall.InitTempData(this);
            AdminAPI_URL += "Course/" + courseId;
            string? accToken = HttpContext.Session.GetString(Constants.ACC_TOKEN);

            var courseInfoResponse = await methcall.CallMethodReturnObject<AdminCourseInfoResponseVM>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.GET_METHOD,
                url: AdminAPI_URL,
                accessToken: accToken,
                _logger: _logger);

            if (courseInfoResponse == null)
            {
                _logger.LogError("Error while getting course info");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while getting course info !";

                return Redirect("/Admin/Course/Index");
            }
            if (!courseInfoResponse.Status || courseInfoResponse.Data == null)
            {
                _logger.LogError("Error while getting course info");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while getting course info !";

                return Redirect("/Admin/Course/Index");
            }
            TempData[Constants.ALERT_DEFAULT_SUCCESS_NAME] = "Course Info Get Successfully!";

            var createSessionFailed = methcall.GetValidationTempData<CreateSessionVM>(this, TempData, Constants.CREATE_SESSION_DETAILS_VALID, "createSession", jsonOptions);
            //var createHomeworkFailed = methcall.GetValidationTempData<CreateHomeworkVM>(this, TempData, Constants.CREATE_HOMEWORK_DETAILS_VALID, "createHomework", jsonOptions);
            var updateCourseFailed = methcall.GetValidationTempData<UpdateCourseVM>(this, TempData, Constants.UPDATE_COURSE_DETAILS_VALID, "updateCourse", jsonOptions);
            var updateSessionFailed = methcall.GetValidationTempData<UpdateSessionVM>(this, TempData, Constants.UPDATE_SESSION_DETAILS_VALID, "updateSession", jsonOptions);

            var sessionList = new List<AdminSessionInfoPVM>();

            if (courseInfoResponse.Data.Sessions != null && courseInfoResponse.Data.Sessions.Count > 0)
                foreach (var session in courseInfoResponse.Data.Sessions)
                {
                    sessionList.Add(new AdminSessionInfoPVM(
                        courseId,
                        _mapper.Map<SessionInfoVM>(session),
                        updateSessionFailed != null && updateSessionFailed.SessionId.Equals(session.SessionId) ? updateSessionFailed : _mapper.Map<UpdateSessionVM>(session)
                        )
                    );
                }
            CourseInfo = _mapper.Map<CourseInfoVM>(courseInfoResponse.Data);
            UpdateCourseInfo = updateCourseFailed ?? _mapper.Map<UpdateCourseVM>(courseInfoResponse.Data);
            CreateSession = createSessionFailed ?? new CreateSessionVM();
            Sessions = sessionList ?? new List<AdminSessionInfoPVM>();
            return Page();
        }
        public async Task<IActionResult> OnPostUpdate(
            [FromRoute][Required] int courseId,
            [FromForm][Required] UpdateCourseVM updateCourse)
        {
            AdminAPI_URL += "Course/" + courseId + "/Update";
            string? accToken = HttpContext.Session.GetString(Constants.ACC_TOKEN);
            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.UPDATE_COURSE_DETAILS_VALID, updateCourse, jsonOptions);
                return Redirect("/Admin/Course/" + courseId + "/Info");
            }
            var courseInfoResponse = await methcall.CallMethodReturnObject<AdminCourseUpdateResponseVM>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.PUT_METHOD,
                url: AdminAPI_URL,
                accessToken: accToken,
                inputType: _mapper.Map<CourseViewModel>(updateCourse),
                _logger: _logger);

            if (courseInfoResponse == null)
            {
                _logger.LogError("Error while getting course info");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while getting course info !";

                return Redirect("/Admin/Course/" + courseId + "/Info");
            }
            if (!courseInfoResponse.Status)
            {
                _logger.LogError("Error while getting course info");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while getting course info !";

                return Redirect("/Admin/Course/" + courseId + "/Info");
            }
            TempData[Constants.ALERT_DEFAULT_SUCCESS_NAME] = "Course Info Update Successfully!";

            return Redirect("/Admin/Course/" + courseId + "/Info");
        }
        public async Task<IActionResult> OnPostCreate(
            [FromRoute][Required] int courseId,
            [FromForm][Required] CreateSessionVM createSession)
        {
            AdminAPI_URL += "Session/Create";
            string? accToken = HttpContext.Session.GetString(Constants.ACC_TOKEN);

            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.CREATE_SESSION_DETAILS_VALID, createSession, jsonOptions);
                return Redirect("/Admin/Course/" + courseId + "/Info");
            }

            var authenResponse = await methcall.CallMethodReturnObject<AdminSessionCreateResponseVM>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.POST_METHOD,
                url: AdminAPI_URL,
                inputType: _mapper.Map<CreateNewSession>(createSession),
                accessToken: accToken,
                _logger: _logger);

            if (authenResponse == null)
            {
                _logger.LogError("Error while registering Session");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while registering Session !";

                return Redirect("/Admin/Course/" + courseId + "/Info");
            }
            if (!authenResponse.Status)
            {
                _logger.LogError("Error while registering Session");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while registering Session !";

                return Redirect("/Admin/Course/" + courseId + "/Info");
            }
            TempData[Constants.ALERT_DEFAULT_SUCCESS_NAME] = "Session Create Successfully!";
            return Redirect("/Admin/Course/" + courseId + "/Info");
        }
        public async Task<IActionResult> OnPostSessionUpdate(
            [FromRoute][Required] int courseId,
            [Required] int sessionId,
            [FromForm][Required] UpdateSessionVM updateSession)
        {
            AdminAPI_URL += "Session/" + sessionId + "/Update";
            string? accToken = HttpContext.Session.GetString(Constants.ACC_TOKEN);
            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.UPDATE_SESSION_DETAILS_VALID, updateSession, jsonOptions);
                return Redirect("/Admin/Course/" + courseId + "/Info");
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

                return Redirect("/Admin/Course/" + courseId + "/Info");
            }
            if (!courseInfoResponse.Status)
            {
                _logger.LogError("Error while getting session info");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while getting session info !";

                return Redirect("/Admin/Course/" + courseId + "/Info");
            }
            TempData[Constants.ALERT_DEFAULT_SUCCESS_NAME] = "Session Info Update Successfully!";

            return Redirect("/Admin/Course/" + courseId + "/Info");
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
