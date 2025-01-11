using AutoMapper;
using CEG_RazorWebApp.Libraries;
using CEG_RazorWebApp.Models.Homework.Get;
using CEG_RazorWebApp.Models.Student.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace CEG_RazorWebApp.Pages.Student.Homework
{
    public class HomeworkIndexModel : PageModel
    {
        private readonly ILogger<HomeworkIndexModel> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient = null;
        private string StudentAPI_URL = "";
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
        [BindProperty]
        public List<HomeworkInfoVM>? Homeworks { get; set; }
        public string? LayoutUrl { get; set; } = Constants.STUDENT_LAYOUT_URL;
        //[BindProperty]
        //public CreateClassVM? CreateClass { get; set; } = new CreateClassVM();
        public HomeworkIndexModel(ILogger<HomeworkIndexModel> logger, IConfiguration config, IMapper mapper)
        {
            _logger = logger;
            _config = config;
            _mapper = mapper;
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(config.GetSection(Constants.SYSTEM_DEFAULT_API_HTTPS_URL_CONFIG_PATH).Value)
            };
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            StudentAPI_URL = config.GetSection(Constants.SYSTEM_DEFAULT_API_URL_CONFIG_PATH).Value;
        }
        public IActionResult OnGetInfo(
            [Required] int homeworkId, int studentId)
        {
            return Redirect("/Student" + studentId + "/Homework/" + homeworkId + "/Info");
        }

        public async Task<IActionResult> OnGetAsync()
        {
            methcall.InitTempData(this);
            StudentAPI_URL += "Homework/All";
            string? accToken = HttpContext.Session.GetString(Constants.ACC_TOKEN);

            var ClassListResponse = await methcall.CallMethodReturnObject<StudentHomeworkListReponseVM>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.GET_METHOD,
                url: StudentAPI_URL,
                accessToken: accToken,
                _logger: _logger);

            if (ClassListResponse == null)
            {
                _logger.LogError("Error while getting Homework list");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while getting Homework list !";

                return Redirect("/Student/Index");
            }
            if (!ClassListResponse.Status)
            {
                _logger.LogError("Error while getting Homework list");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while getting Homework list !";

                return Redirect("/Student/Index");
            }
            /*TempData[Constants.ALERT_DEFAULT_SUCCESS_NAME] = ViewBag.Success = "Class List Get Successfully!";*/
            TempData[Constants.ALERT_DEFAULT_SUCCESS_NAME] = "Homework List Get Successfully!";

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
    }
}

