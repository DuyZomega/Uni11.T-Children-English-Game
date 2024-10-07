using AutoMapper;
using CEG_RazorWebApp.Libraries;
using CEG_RazorWebApp.Models.Admin.Response;
using CEG_RazorWebApp.Models.Course.Create;
using CEG_RazorWebApp.Models.Course.Get;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace CEG_RazorWebApp.Pages.Admin.Course
{
    public class CourseIndexModel : PageModel
    {
        private readonly ILogger<CourseIndexModel> _logger;
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
        public string? LayoutUrl { get; set; } = Constants.ADMIN_LAYOUT_URL;
        private readonly ChildrenEnglishGameLibrary methcall = new();
        [BindProperty]
        public CreateCourseVM? CreateCourse { get; set; }
        public List<IndexCourseInfoVM>? Courses { get; set; }

        public CourseIndexModel(ILogger<CourseIndexModel> logger, IConfiguration config, IMapper mapper)
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
        public IActionResult OnGetInfo(
            [Required] int courseId)
        {
            return Redirect("/Admin/Course/" + courseId + "/Info");
        }

        public async Task<IActionResult> OnGetAsync()
        {
            methcall.InitTempData(this);
            AdminAPI_URL += "Course/All";
            string? accToken = HttpContext.Session.GetString(Constants.ACC_TOKEN);

            var courseListResponse = await methcall.CallMethodReturnObject<AdminCourseListResponseVM>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.GET_METHOD,
                url: AdminAPI_URL,
                accessToken: accToken,
                _logger: _logger);

            if (courseListResponse == null)
            {
                _logger.LogError("Error while getting course list");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while getting course list !";

                return Redirect("/Admin/Index");
            }
            if (!courseListResponse.Status)
            {
                _logger.LogError("Error while getting course list");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while getting course list !";

                return Redirect("/Admin/Index");
            }
            /*TempData[Constants.ALERT_DEFAULT_SUCCESS_NAME] = ViewBag.Success = "Course List Get Successfully!";*/
            TempData[Constants.ALERT_DEFAULT_SUCCESS_NAME] = "Course List Get Successfully!";
            var courseTempData = methcall.GetValidationTempData<CreateCourseVM>(this, TempData, Constants.CREATE_COURSE_DETAILS_VALID, "createCourse", jsonOptions);

            Courses = _mapper.Map<List<IndexCourseInfoVM>>(courseListResponse.Data);
            CreateCourse = courseTempData != null ? courseTempData : new CreateCourseVM();

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
