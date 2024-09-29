using CEG_RazorWebApp.Libraries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace CEG_RazorWebApp.Pages.Home
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient = null;
        private string HomeAPI_URL = "";
        private JsonSerializerOptions options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            PropertyNameCaseInsensitive = true,
        };
        private ChildrenEnglishGameLibrary methcall = new();

        public IndexModel(ILogger<IndexModel> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(config.GetSection(Constants.SYSTEM_DEFAULT_API_HTTPS_URL_CONFIG_PATH).Value)
            };
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HomeAPI_URL = config.GetSection(Constants.SYSTEM_DEFAULT_API_URL_CONFIG_PATH).Value;
        }

        public async Task<IActionResult> OnGet()
        {
            /*string MeetingAPI_URL = HomeAPI_URL + "Meeting/All";
            string FieldTripAPI_URL_All = HomeAPI_URL + "FieldTrip/All";
            string ContestAPI_URL_All = HomeAPI_URL + "Contest/All";
            dynamic testmodel = new ExpandoObject();
            if (_httpClient.DefaultRequestHeaders.Authorization != null)
            {
                var token = _httpClient.DefaultRequestHeaders.Authorization.Parameter;
            }
            *//*string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Member")) return RedirectToAction("Logout", "Home");
            
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            string? usrname = HttpContext.Session.GetString("USER_NAME");
            if (string.IsNullOrEmpty(usrname)) return RedirectToAction("Login", "Auth");*//*
            string? usrId = HttpContext.Session.GetString("USER_ID");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (role == null) role = "Guest";

            string? usrname = HttpContext.Session.GetString("USER_NAME");

            string? imagepath = HttpContext.Session.GetString("IMAGE_PATH");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;
            TempData["IMAGE_PATH"] = imagepath;

            if (usrId != null)
            {
                string NotificationCountAPI_URL = "/api/Notification/Count";
                string NotificationTitleAPI_URL = "/api/Notification/Unread";

                var notificationCount = await methcall.CallMethodReturnObject<GetNotificationCountResponse>(
                _httpClient: _httpClient,
                options: options,
                methodName: "POST",
                url: NotificationCountAPI_URL,
                inputType: usrId,
                _logger: _logger);

                var notificationTitle = await methcall.CallMethodReturnObject<GetNotificationTitleResponse>(
                _httpClient: _httpClient,
                options: options,
                methodName: "POST",
                url: NotificationTitleAPI_URL,
                inputType: usrId,
                _logger: _logger);

                ViewBag.NotificationCount = notificationCount.IntData;
                ViewBag.NotificationTitle = notificationTitle.Data.ToList();
            }

            var listFieldTripResponse = await methcall.CallMethodReturnObject<GetFieldTripResponseByList>(
                _httpClient: _httpClient,
                options: options,
                methodName: "POST",
                url: FieldTripAPI_URL_All,
                inputType: role,
                _logger: _logger);

            var listMeetResponse = await methcall.CallMethodReturnObject<GetMeetingResponseByList>(
                _httpClient: _httpClient,
                options: options,
                methodName: "POST",
                url: MeetingAPI_URL,
                inputType: role,
                _logger: _logger);

            var listContestResponse = await methcall.CallMethodReturnObject<GetContestResponseByList>(
                _httpClient: _httpClient,
                options: options,
                methodName: "POST",
                url: ContestAPI_URL_All,
                inputType: role,
                _logger: _logger);

            if (listMeetResponse == null || listFieldTripResponse == null || listContestResponse == null)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting List Meeting Or Fieldtrip!).\n List was Empty!";
                Redirect("~/Home/Error");
            }
            else
            if (!listMeetResponse.Status || !listFieldTripResponse.Status || !listContestResponse.Status)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting List Meeting Or Fieldtrip!).\n"
                    + listMeetResponse.ErrorMessage + "\n" + listFieldTripResponse.ErrorMessage;
                Redirect("~/Home/Error");
            }
            testmodel.Meetings = listMeetResponse.Data;
            testmodel.FieldTrips = listFieldTripResponse.Data;
            testmodel.Contests = listContestResponse.Data;
            return View(testmodel);*/

            return Page();
        }
    }
}
