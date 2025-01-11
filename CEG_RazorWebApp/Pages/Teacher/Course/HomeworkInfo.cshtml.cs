using AutoMapper;
using CEG_BAL.ViewModels;
using CEG_DAL.Models;
using CEG_RazorWebApp.Libraries;
using CEG_RazorWebApp.Models.Teacher.Response;
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

namespace CEG_RazorWebApp.Pages.Teacher.Course
{
    public class HomeworkInfoModel : PageModel
    {
        private readonly ILogger<HomeworkInfoModel> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient = null;
        private string TeacherAPI_URL = "";
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
        public string? LayoutUrl { get; set; } = Constants.TEACHER_LAYOUT_URL;
        [BindProperty]
        public int? CourseId { get; set; }
        [BindProperty]
        public int? SessionId { get; set; }
        public int? HomeworkId { get; set; }
        public HomeworkInfoVM? HomeworkInfo { get; set; }
        public UpdateHomeworkVM? UpdateHomeworkInfo { get; set; }
        public UpdateQuestionVM? AddQuestion { get; set; } = new UpdateQuestionVM();
        public List<AdminQuestionInfoPVM>? Questions { get; set; }
        public string? AccToken;
        public string? ApiUrl;
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
            TeacherAPI_URL = config.GetSection(Constants.SYSTEM_DEFAULT_API_URL_CONFIG_PATH).Value;
            ApiUrl = _config.GetSection(Constants.SYSTEM_DEFAULT_API_HTTPS_URL_CONFIG_PATH).Value + _config.GetSection(Constants.SYSTEM_DEFAULT_API_URL_CONFIG_PATH).Value;
        }
        public void OnGetAsync(
            [FromRoute][Required] int courseId,
            [FromRoute][Required] int sessionId,
            [FromRoute][Required] int homeworkId)
        {
            methcall.InitTempData(this);
            CourseId = courseId;
            SessionId = sessionId;
            AccToken = HttpContext.Session.GetString(Constants.ACC_TOKEN);
            HomeworkId = homeworkId;
        }
        //public async Task<IActionResult> OnPostQuestionUpdate(
        //    [FromRoute][Required] int homeworkId,
        //    [Required] int questionId,
        //    [FromForm][Required] UpdateQuestionVM updateQuestion)
        //{
        //    TeacherAPI_URL += "Question/" + questionId + "/Update";
        //    string? accToken = HttpContext.Session.GetString(Constants.ACC_TOKEN);
        //    if (!ModelState.IsValid)
        //    {
        //        TempData = methcall.SetValidationTempData(TempData, Constants.UPDATE_HOMEWORK_QUESTION_DETAILS_VALID, updateQuestion, jsonOptions);
        //        return Redirect("/Teacher/Course/" + CourseId + "/Session/" + SessionId + "/Homework/" + homeworkId + "/Info");
        //    }
        //    var courseInfoResponse = await methcall.CallMethodReturnObject<AdminQuestionUpdateResponseVM>(
        //        _httpClient: _httpClient,
        //        options: jsonOptions,
        //        methodName: Constants.PUT_METHOD,
        //        url: TeacherAPI_URL,
        //        accessToken: accToken,
        //        inputType: _mapper.Map<HomeworkQuestionViewModel>(updateQuestion),
        //        _logger: _logger);

        //    if (courseInfoResponse == null)
        //    {
        //        _logger.LogError("Error while updating question info");

        //        TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while updating question info !";

        //        return Redirect("/Teacher/Course/" + CourseId + "/Session/" + SessionId + "/Homework/" + homeworkId + "/Info");
        //    }
        //    if (!courseInfoResponse.Status)
        //    {
        //        _logger.LogError("Error while updating question info");

        //        TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while updating question info !";

        //        return Redirect("/Teacher/Course/" + CourseId + "/Session/" + SessionId + "/Homework/" + homeworkId + "/Info");
        //    }
        //    TempData[Constants.ALERT_DEFAULT_SUCCESS_NAME] = "Question Update Successfully!";

        //    return Redirect("/Teacher/Course/" + CourseId + "/Session/" + SessionId + "/Homework/" + homeworkId + "/Info");
        //}
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
