using CEG_BAL.ViewModels;
using CEG_WebMVC.Library;
using CEG_WebMVC.Models.ViewModels.Admin;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace CEG_WebMVC.Controllers
{
    [Route("Admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
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
        private BirdClubLibrary methcall = new();

        public AdminController(ILogger<AdminController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            /*_httpClient.BaseAddress = new Uri(config.GetSection("DefaultApiUrl:ConnectionString").Value);
            AdminAPI_URL = "/api/";*/
        }

        [HttpGet("Index")]
        public async Task<IActionResult> AdminIndex()
        {
            /*if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.ADMIN) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.ADMIN));*/
            return View();
        }

        [HttpGet("AccountIndex")]
        public async Task<IActionResult> AdminAccountIndex()
        {
            /*if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.ADMIN) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.ADMIN));*/
            var adminAccounts = new AdminAccountIndexVM();
            return View(adminAccounts);
        }

        [HttpGet("Profile")]
        public async Task<IActionResult> AdminProfile()
        {
            AdminAPI_URL += "Admin/Profile";

            /*if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.ADMIN) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.ADMIN));*/

            string? accToken = HttpContext.Session.GetString(Constants.ACC_TOKEN);
            string? usrId = HttpContext.Session.GetString(Constants.USR_ID);
            string? imagePath = HttpContext.Session.GetString(Constants.USR_IMAGE);

            var adminDataAndErrors = new AdminProfileVM();
            /*var memberInvalidDetails = methcall.GetValidationTempData<AccountViewModel>(this, TempData, Constants.UPDATE_ADMIN_DETAILS_VALID, "adminDetail", jsonOptions);
            if (memberInvalidDetails != null)
            {
                memberInvalidDetails.ImagePath = imagePath;
                memberInvalidDetails.DefaultUserGenderSelectList = methcall.GetUserGenderSelectableList(memberInvalidDetails.Gender != null ? memberInvalidDetails.Gender : Constants.MALE);
                adminInvalids.adminDetail = memberInvalidDetails;
                return View(adminInvalids);
            }*/

            /*var adminDetails = await methcall.CallMethodReturnObject<GetMemberProfileResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.POST_METHOD,
                url: AdminAPI_URL,
                _logger: _logger,
                inputType: usrId,
                accessToken: accToken);
            if (adminDetails == null || adminDetails.Data == null)
            {
                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] =
                    "Error while processing your request! (Getting Admin Profile!).\n Admin Details Not Found!";
                return RedirectToAction("AdminIndex");
            }
            else
            if (!adminDetails.Status)
            {
                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] =
                    "Error while processing your request! (Getting Admin Profile!).\n Admin Details Not Found!"
                + adminDetails.ErrorMessage;
                return RedirectToAction("AdminIndex");
            }
            var adminInvalidPasswordUpdate = methcall.GetValidationTempData<UpdateMemberPassword>(this, TempData, Constants.UPDATE_ADMIN_PASSWORD_VALID, "adminPassword", jsonOptions);
            if (adminInvalidPasswordUpdate != null)
            {
                adminInvalids.adminPassword = adminInvalidPasswordUpdate;
            }
            adminDetails.Data.DefaultUserGenderSelectList = methcall.GetUserGenderSelectableList(adminDetails.Data.Gender);
            adminInvalids.adminDetail = adminDetails.Data;*/
            return View(adminDataAndErrors);
        }
    }
}
