using AutoMapper;
using CEG_RazorWebApp.Libraries;
using CEG_RazorWebApp.Models.Teacher.Response;
using CEG_RazorWebApp.Models.Class.Create;
using CEG_RazorWebApp.Models.Class.Get;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Text.Json;
using CEG_DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace CEG_RazorWebApp.Pages.Teacher.Class
{
    public class ClassIndexModel : PageModel
    {
        private readonly ILogger<ClassIndexModel> _logger;
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
        [BindProperty]
        public List<IndexClassInfoVM>? Classes { get; set; }
        public string? LayoutUrl { get; set; } = Constants.TEACHER_LAYOUT_URL;
        //[BindProperty]
        //public CreateClassVM? CreateClass { get; set; } = new CreateClassVM();
        public ClassIndexModel(ILogger<ClassIndexModel> logger, IConfiguration config, IMapper mapper)
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
        }
        public IActionResult OnGetInfo(
            [Required] int ClassId,int TeacherId)
        {
            return Redirect("/Teacher" + TeacherId + "/Class/" + ClassId + "/Info");
        }

        public async Task<IActionResult> OnGetAsync()
        {
            methcall.InitTempData(this);
            TeacherAPI_URL += "Class/All";
            string? accToken = HttpContext.Session.GetString(Constants.ACC_TOKEN);

            var ClassListResponse = await methcall.CallMethodReturnObject<TeacherClassListResponseVM>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.GET_METHOD,
                url: TeacherAPI_URL,
                accessToken: accToken,
                _logger: _logger);

            if (ClassListResponse == null)
            {
                _logger.LogError("Error while getting Class list");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while getting Class list !";

                return Redirect("/Teacher/Index");
            }
            if (!ClassListResponse.Status)
            {
                _logger.LogError("Error while getting Class list");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while getting Class list !";

                return Redirect("/Teacher/Index");
            }
            /*TempData[Constants.ALERT_DEFAULT_SUCCESS_NAME] = ViewBag.Success = "Class List Get Successfully!";*/
            TempData[Constants.ALERT_DEFAULT_SUCCESS_NAME] = "Class List Get Successfully!";

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
