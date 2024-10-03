using AutoMapper;
using CEG_DAL.Models;
using CEG_RazorWebApp.Libraries;
using CEG_RazorWebApp.Models.Admin.Response;
using CEG_RazorWebApp.Models.Homework.Get;
using CEG_RazorWebApp.Models.Homework.Update;
using CEG_RazorWebApp.Models.HomeworkQuestion.Create;
using CEG_RazorWebApp.Models.HomeworkQuestion.Get;
using CEG_RazorWebApp.Models.HomeworkQuestion.Update;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace CEG_RazorWebApp.Pages.Admin.Course
{
    public class HomeworkInfoModel : PageModel
    {
        private readonly ILogger<HomeworkInfoModel> _logger;
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
        [BindProperty]
        public int? SessionId { get; set; }
        public HomeworkInfoVM? HomeworkInfo { get; set; }
        public UpdateHomeworkVM? UpdateHomeworkInfo { get; set; }
        public CreateQuestionVM? CreateQuestion { get; set; }
        public List<AdminQuestionInfoPVM>? Questions { get; set; }
        public HomeworkInfoModel(ILogger<HomeworkInfoModel> logger, IConfiguration config, IMapper mapper)
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
            [FromRoute][Required] int sessionId,
            [FromRoute][Required] int homeworkId)
        {
            methcall.InitTempData(this);
            CourseId = courseId;
            SessionId = sessionId;
            AdminAPI_URL += "Homework/" + homeworkId;
            string? accToken = HttpContext.Session.GetString(Constants.ACC_TOKEN);

            var homeworkInfoResponse = await methcall.CallMethodReturnObject<AdminHomeworkInfoResponseVM>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.GET_METHOD,
                url: AdminAPI_URL,
                accessToken: accToken,
                _logger: _logger);

            if (homeworkInfoResponse == null)
            {
                _logger.LogError("Error while getting session info");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while getting session info !";

                return Redirect("/Admin/Course/" + CourseId + "/Session/" + SessionId + "/Info");
            }
            if (!homeworkInfoResponse.Status || homeworkInfoResponse.Data == null)
            {
                _logger.LogError("Error while getting course info");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while getting session info !";

                return Redirect("/Admin/Course/" + CourseId + "/Session/" + SessionId + "/Info");
            }
            if (!TempData.ContainsKey(Constants.ALERT_DEFAULT_ERROR_NAME) || !TempData.ContainsKey(Constants.ALERT_DEFAULT_SUCCESS_NAME))
            {
                TempData[Constants.ALERT_DEFAULT_SUCCESS_NAME] = "Session Info Get Successfully!";
            }

            var createQuestionFailed = methcall.GetValidationTempData<CreateQuestionVM>(this, TempData, Constants.CREATE_HOMEWORK_DETAILS_VALID, "createHomework", jsonOptions);
            var updateHomeworkFailed = methcall.GetValidationTempData<UpdateHomeworkVM>(this, TempData, Constants.UPDATE_HOMEWORK_DETAILS_VALID, "updateHomework", jsonOptions);
            var updateQuestionFailed = methcall.GetValidationTempData<UpdateQuestionVM>(this, TempData, Constants.UPDATE_HOMEWORK_DETAILS_VALID, "updateHomework", jsonOptions);
            var questionList = new List<AdminQuestionInfoPVM>();

            if (homeworkInfoResponse.Data.HomeworkQuestions != null && homeworkInfoResponse.Data.HomeworkQuestions.Count > 0)
                foreach (var question in homeworkInfoResponse.Data.HomeworkQuestions)
                {
                    questionList.Add(new AdminQuestionInfoPVM(
                        CourseId,
                        SessionId,
                        homeworkId,
                        _mapper.Map<QuestionInfoVM>(question),
                        updateQuestionFailed != null && updateQuestionFailed.HomeworkQuestionId.Equals(question.HomeworkQuestionId) ? updateQuestionFailed : _mapper.Map<UpdateQuestionVM>(question)
                        )
                    );
                }
            HomeworkInfo = _mapper.Map<HomeworkInfoVM>(homeworkInfoResponse.Data);
            UpdateHomeworkInfo = updateHomeworkFailed ?? _mapper.Map<UpdateHomeworkVM>(homeworkInfoResponse.Data);
            Questions = questionList ?? new List<AdminQuestionInfoPVM>();
            CreateQuestion = createQuestionFailed ?? new CreateQuestionVM();
            return Page();
        }
    }
}
