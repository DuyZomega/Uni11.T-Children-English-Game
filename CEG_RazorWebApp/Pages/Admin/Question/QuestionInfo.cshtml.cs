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

namespace CEG_RazorWebApp.Pages.Admin.Question
{
    public class QuestionInfoModel : PageModel
    {
        private readonly IConfiguration _config;
        private readonly ILogger<QuestionInfoModel> _logger;
        private readonly IMapper _mapper;
        /*
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
        [BindProperty]
        public int? CourseId { get; set; }
        [BindProperty]
        public int? SessionId { get; set; }
        [BindProperty]
        public int? HomeworkId { get; set; }*/
        private ChildrenEnglishGameLibrary methcall = new();
        public int? QuestionId { get; set; }
        public string? AccToken;
        public string? ApiUrl;
        public string? LayoutUrl { get; set; } = Constants.ADMIN_LAYOUT_URL;
        public UpdateQuestionVM? UpdateQuestionInfo { get; set; } = new UpdateQuestionVM();
        public CreateAnswerVM? CreateAnswer { get; set; } = new CreateAnswerVM();
        /*public QuestionInfoVM? QuestionInfo { get; set; }
        public CreateAnswerVM? CreateAnswer { get; set; }
        public List<AdminAnswerInfoPVM>? Answers { get; set; }*/
        public QuestionInfoModel(ILogger<QuestionInfoModel> logger, IConfiguration config, IMapper mapper)
        {
            _logger = logger;
            _config = config;
            _mapper = mapper;
            /*_httpClient = new HttpClient()
            {
                BaseAddress = new Uri(config.GetSection(Constants.SYSTEM_DEFAULT_API_HTTPS_URL_CONFIG_PATH).Value)
            };
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));*/
            ApiUrl = _config.GetSection(Constants.SYSTEM_DEFAULT_API_HTTPS_URL_CONFIG_PATH).Value + _config.GetSection(Constants.SYSTEM_DEFAULT_API_URL_CONFIG_PATH).Value;
        }
        public void OnGet(
            [FromRoute][Required] int questionId)
        {
            methcall.InitTempData(this);
            AccToken = HttpContext.Session.GetString(Constants.ACC_TOKEN);
            QuestionId = questionId;
        }
        /*
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
        */
        /*public async Task<IActionResult> OnGetAnswerDetails(HomeworkAnswerViewModel answer)
        {
            var answerObject = new AdminAnswerInfoPVM(
                questionId: answer.HomeworkQuestion.HomeworkQuestionId,
                answerInfo: _mapper.Map<AnswerInfoVM>(answer),
                updateAnswerInfo: _mapper.Map<UpdateAnswerVM>(answer)
                );
            return Partial("PartialViews/_AnswerDetails", answerObject);
        }*/
        public IActionResult OnGetLogout()
        {
            //_httpClient.DefaultRequestHeaders.Authorization = null;
            HttpContext.Session.Clear();
            TempData.Clear();
            SignOut();

            // If using ASP.NET Identity, you may want to sign out the user
            // Example: await SignInManager.SignOutAsync();

            return RedirectToPage(Constants.LOGOUT_REDIRECT_URL);
        }
    }
}
