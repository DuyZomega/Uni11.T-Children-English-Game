using AutoMapper;
using CEG_BAL.ViewModels.Admin;
using CEG_BAL.ViewModels;
using CEG_RazorWebApp.Libraries;
using CEG_RazorWebApp.Models.Admin.Response;
using CEG_RazorWebApp.Models.HomeworkAnswer.Create;
using CEG_RazorWebApp.Models.HomeworkAnswer.Get;
using CEG_RazorWebApp.Models.HomeworkQuestion.Create;
using CEG_RazorWebApp.Models.HomeworkQuestion.Get;
using CEG_RazorWebApp.Models.HomeworkQuestion.Update;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;
using CEG_RazorWebApp.Models.HomeworkAnswer.Update;

namespace CEG_RazorWebApp.Pages.Admin.Course
{
    public class HomeworkQuestionInfoModel : PageModel
    {
        private readonly ILogger<HomeworkQuestionInfoModel> _logger;
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
        [BindProperty]
        public int? HomeworkId { get; set; }
        public QuestionInfoVM? QuestionInfo { get; set; }
        public UpdateQuestionVM? UpdateQuestionInfo { get; set; }
        public CreateAnswerVM? CreateAnswer { get; set; }
        public List<AdminAnswerInfoPVM>? Answers { get; set; }
        public HomeworkQuestionInfoModel(ILogger<HomeworkQuestionInfoModel> logger, IConfiguration config, IMapper mapper)
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
            [FromRoute][Required] int homeworkId,
            [FromRoute][Required] int questionId)
        {
            methcall.InitTempData(this);
            CourseId = courseId;
            SessionId = sessionId;
            HomeworkId = homeworkId;
            AdminAPI_URL += "Question/" + questionId;
            string? accToken = HttpContext.Session.GetString(Constants.ACC_TOKEN);

            var questionInfoResponse = await methcall.CallMethodReturnObject<AdminQuestionInfoResponseVM>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.GET_METHOD,
                url: AdminAPI_URL,
                accessToken: accToken,
                _logger: _logger);

            if (questionInfoResponse == null)
            {
                _logger.LogError("Error while getting question info");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while getting question info !";

                return Redirect("/Admin/Course/" + CourseId + "/Session/" + SessionId + "/Homework/" + HomeworkId + "/Info");
            }
            if (!questionInfoResponse.Status || questionInfoResponse.Data == null)
            {
                _logger.LogError("Error while getting course info");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while getting question info !";

                return Redirect("/Admin/Course/" + CourseId + "/Session/" + SessionId + "/Homework/" + HomeworkId + "/Info");
            }
            TempData[Constants.ALERT_DEFAULT_SUCCESS_NAME] = "Homework Question Info Get Successfully!";

            var createAnswerFailed = methcall.GetValidationTempData<CreateAnswerVM>(this, TempData, Constants.CREATE_HOMEWORK_DETAILS_VALID, "createAnswer", jsonOptions);
            var updateQuestionFailed = methcall.GetValidationTempData<UpdateQuestionVM>(this, TempData, Constants.UPDATE_HOMEWORK_QUESTION_DETAILS_VALID, "updateQuestion", jsonOptions);
            var updateAnswerFailed = methcall.GetValidationTempData<UpdateAnswerVM>(this, TempData, Constants.UPDATE_HOMEWORK_ANSWER_DETAILS_VALID, "updateAnswer", jsonOptions);
            var answerList = new List<AdminAnswerInfoPVM>();

            if (questionInfoResponse.Data.HomeworkAnswers != null && questionInfoResponse.Data.HomeworkAnswers.Count > 0)
                foreach (var answer in questionInfoResponse.Data.HomeworkAnswers)
                {
                    answerList.Add(new AdminAnswerInfoPVM(
                        CourseId,
                        SessionId,
                        HomeworkId,
                        questionId,
                        questionInfoResponse.Data.HomeworkStatus,
                        _mapper.Map<AnswerInfoVM>(answer),
                        updateAnswerFailed != null && updateAnswerFailed.HomeworkAnswerId.Equals(answer.HomeworkAnswerId) ? updateAnswerFailed : _mapper.Map<UpdateAnswerVM>(answer)
                        )
                    );
                }
            QuestionInfo = _mapper.Map<QuestionInfoVM>(questionInfoResponse.Data);
            UpdateQuestionInfo = updateQuestionFailed ?? _mapper.Map<UpdateQuestionVM>(questionInfoResponse.Data);
            Answers = answerList ?? new List<AdminAnswerInfoPVM>();
            CreateAnswer = createAnswerFailed ?? new CreateAnswerVM();
            return Page();
        }
        public async Task<IActionResult> OnPostUpdate(
            [FromRoute][Required] int questionId,
            [FromForm][Required] UpdateQuestionVM updateQuestion)
        {
            AdminAPI_URL += "Question/" + questionId + "/Update";
            string? accToken = HttpContext.Session.GetString(Constants.ACC_TOKEN);
            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.UPDATE_HOMEWORK_QUESTION_DETAILS_VALID, updateQuestion, jsonOptions);
                return Redirect("/Admin/Course/" + CourseId + "/Session/" + SessionId + "/Homework/" + HomeworkId + "/Question/" + questionId + "/Info");
            }
            var courseInfoResponse = await methcall.CallMethodReturnObject<AdminQuestionUpdateResponseVM>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.PUT_METHOD,
                url: AdminAPI_URL,
                accessToken: accToken,
                inputType: _mapper.Map<HomeworkQuestionViewModel>(updateQuestion),
                _logger: _logger);

            if (courseInfoResponse == null)
            {
                _logger.LogError("Error while updating question info");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while updating question info !";

                return Redirect("/Admin/Course/" + CourseId + "/Session/" + SessionId + "/Homework/" + HomeworkId + "/Question/" + questionId + "/Info");
            }
            if (!courseInfoResponse.Status)
            {
                _logger.LogError("Error while updating question info");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while updating question info !";

                return Redirect("/Admin/Course/" + CourseId + "/Session/" + SessionId + "/Homework/" + HomeworkId + "/Question/" + questionId + "/Info");
            }
            TempData[Constants.ALERT_DEFAULT_SUCCESS_NAME] = "Question Update Successfully!";

            return Redirect("/Admin/Course/" + CourseId + "/Session/" + SessionId + "/Homework/" + HomeworkId + "/Question/" + questionId + "/Info");
        }
        public async Task<IActionResult> OnPostCreate(
            [FromRoute][Required] int questionId,
            [FromForm][Required] CreateAnswerVM createAnswer)
        {
            AdminAPI_URL += "Answer/Create";
            string? accToken = HttpContext.Session.GetString(Constants.ACC_TOKEN);

            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.CREATE_HOMEWORK_ANSWER_DETAILS_VALID, createAnswer, jsonOptions);
                return Redirect("/Admin/Course/" + CourseId + "/Session/" + SessionId + "/Homework/" + HomeworkId + "/Question/" + questionId + "/Info");
            }

            var authenResponse = await methcall.CallMethodReturnObject<AdminAnswerCreateResponseVM>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.POST_METHOD,
                url: AdminAPI_URL,
                inputType: _mapper.Map<CreateNewAnswer>(createAnswer),
                accessToken: accToken,
                _logger: _logger);

            if (authenResponse == null)
            {
                _logger.LogError("Error while creating answer");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while creating answer !";

                return Redirect("/Admin/Course/" + CourseId + "/Session/" + SessionId + "/Homework/" + HomeworkId + "/Question/" + questionId + "/Info");
            }
            if (!authenResponse.Status)
            {
                _logger.LogError("Error while creating answer");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while creating answer !";

                return Redirect("/Admin/Course/" + CourseId + "/Session/" + SessionId + "/Homework/" + HomeworkId + "/Question/" + questionId + "/Info");
            }
            TempData[Constants.ALERT_DEFAULT_SUCCESS_NAME] = "Homework Answer Create Successfully!";
            return Redirect("/Admin/Course/" + CourseId + "/Session/" + SessionId + "/Homework/" + HomeworkId + "/Question/" + questionId + "/Info");
        }
        public async Task<IActionResult> OnPostAnswerUpdate(
            [FromRoute][Required] int questionId,
            [Required] int answerId,
            [FromForm][Required] UpdateAnswerVM updateAnswer)
        {
            AdminAPI_URL += "Answer/" + answerId + "/Update";
            string? accToken = HttpContext.Session.GetString(Constants.ACC_TOKEN);
            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.UPDATE_HOMEWORK_ANSWER_DETAILS_VALID, updateAnswer, jsonOptions);
                return Redirect("/Admin/Course/" + CourseId + "/Session/" + SessionId + "/Homework/" + HomeworkId + "/Question/" + questionId + "/Info");
            }
            var courseInfoResponse = await methcall.CallMethodReturnObject<AdminAnswerUpdateResponseVM>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.PUT_METHOD,
                url: AdminAPI_URL,
                accessToken: accToken,
                inputType: _mapper.Map<HomeworkAnswerViewModel>(updateAnswer),
                _logger: _logger);

            if (courseInfoResponse == null)
            {
                _logger.LogError("Error while updating answer info");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while updating answer info !";

                return Redirect("/Admin/Course/" + CourseId + "/Session/" + SessionId + "/Homework/" + HomeworkId + "/Question/" + questionId + "/Info");
            }
            if (!courseInfoResponse.Status)
            {
                _logger.LogError("Error while updating answer info");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while updating answer info !";

                return Redirect("/Admin/Course/" + CourseId + "/Session/" + SessionId + "/Homework/" + HomeworkId + "/Question/" + questionId + "/Info");
            }
            TempData[Constants.ALERT_DEFAULT_SUCCESS_NAME] = "Answer Update Successfully!";

            return Redirect("/Admin/Course/" + CourseId + "/Session/" + SessionId + "/Homework/" + HomeworkId + "/Question/" + questionId + "/Info");
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
