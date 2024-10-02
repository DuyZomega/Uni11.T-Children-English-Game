using AutoMapper;
using CEG_RazorWebApp.Libraries;
using CEG_RazorWebApp.Models.Admin.Response;
using CEG_RazorWebApp.Models.Homework.Create;
using CEG_RazorWebApp.Models.Homework.Get;
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

                return Redirect("/Admin/Course/" + courseId + "/Info");
            }
            if (!sessionInfoResponse.Status || sessionInfoResponse.Data == null)
            {
                _logger.LogError("Error while getting course info");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while getting session info !";

                return Redirect("/Admin/Course/" + courseId + "/Info");
            }
            TempData[Constants.ALERT_DEFAULT_SUCCESS_NAME]= "Session Info Get Successfully!";

            var createHomeworkFailed = methcall.GetValidationTempData<CreateHomeworkVM>(this, TempData, Constants.CREATE_HOMEWORK_DETAILS_VALID, "createHomework", jsonOptions);
            var updateSessionFailed = methcall.GetValidationTempData<UpdateSessionVM>(this, TempData, Constants.UPDATE_SESSION_DETAILS_VALID, "updateSession", jsonOptions);
            //var updateHomeworkFailed = methcall.GetValidationTempData<UpdateSessionVM>(this, TempData, Constants.UPDATE_HOMEWORK_DETAILS_VALID, "updateHomework", jsonOptions);
            var homeworkList = new List<AdminHomeworkInfoPVM>();

            if (sessionInfoResponse.Data.Homeworks != null && sessionInfoResponse.Data.Homeworks.Count > 0)
                foreach (var homework in sessionInfoResponse.Data.Homeworks)
                {
                    homeworkList.Add(new AdminHomeworkInfoPVM(
                        courseId,
                        sessionId,
                        _mapper.Map<HomeworkInfoVM>(homework)
                        //updateSessionFailed != null && updateSessionFailed.SessionId.Equals(homework.SessionId) ? updateSessionFailed : _mapper.Map<UpdateSessionVM>(homework)
                        )
                    );
                }

            /*var pageData = new AdminSessionInfoPVM(
                courseId,
                _mapper.Map<SessionInfoVM>(sessionInfoResponse.Data),
                updateSessionFailed ?? _mapper.Map<UpdateSessionVM>(sessionInfoResponse.Data),
                homeworkList,
                createHomeworkFailed
                );*/
            CourseId = courseId;
            SessionInfo = _mapper.Map<SessionInfoVM>(sessionInfoResponse.Data);
            UpdateSessionInfo = updateSessionFailed ?? _mapper.Map<UpdateSessionVM>(sessionInfoResponse.Data);
            Homeworks = homeworkList ?? new List<AdminHomeworkInfoPVM>();
            CreateHomework = createHomeworkFailed ?? new CreateHomeworkVM();
            return Page();
        }
    }
}
