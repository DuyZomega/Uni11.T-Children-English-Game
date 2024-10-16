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
    public class QuestionInfoModel : PageModel
    {
        private readonly IConfiguration _config;
        private readonly ILogger<QuestionInfoModel> _logger;
        private readonly IMapper _mapper;
        private CEG_RAZOR_Library methcall = new();
        public int? CourseId { get; set; }
        public int? HomeworkId { get; set; }
        public int? SessionId { get; set; }
        public int? QuestionId { get; set; }
        public string? AccToken;
        public string? ApiUrl;
        public string? LayoutUrl { get; set; } = Constants.ADMIN_LAYOUT_URL;
        public UpdateQuestionVM? UpdateQuestionInfo { get; set; } = new UpdateQuestionVM();
        public CreateAnswerVM? CreateAnswer { get; set; } = new CreateAnswerVM();
        public QuestionInfoModel(ILogger<QuestionInfoModel> logger, IConfiguration config, IMapper mapper)
        {
            _logger = logger;
            _config = config;
            _mapper = mapper;
            ApiUrl = _config.GetSection(Constants.SYSTEM_DEFAULT_API_HTTPS_URL_CONFIG_PATH).Value + _config.GetSection(Constants.SYSTEM_DEFAULT_API_URL_CONFIG_PATH).Value;
        }
        public void OnGet(
            [FromRoute][Required] int courseId,
            [FromRoute][Required] int sessionId,
            [FromRoute][Required] int homeworkId,
            [FromRoute][Required] int questionId)
        {
            methcall.InitTempData(this);
            AccToken = HttpContext.Session.GetString(Constants.ACC_TOKEN);
            CourseId = courseId;
            SessionId = sessionId;
            HomeworkId = homeworkId;
            QuestionId = questionId;
        }
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
