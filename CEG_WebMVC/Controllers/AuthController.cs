/*using CEG_BAL.ViewModels.Authenticates;
using CEG_BAL.ViewModels.Member;
using CEG_BAL.ViewModels;*/
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using CEG_WebMVC.Services.Interfaces;
using CEG_WebMVC.Models.Auth;
using CEG_WebMVC.Models.VnPay;
/*using CEG_WebMVC.Models.Notification;
using CEG_WebMVC.Models.Transaction;*/
using CEG_BAL.ViewModels.Authenticates;
using CEG_WebMVC.Models.ViewModels.Account.Response;
using CEG_WebMVC.Libraries;
namespace CEG_WebMVC.Controllers
{
    [Route("Auth")]
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _config;
        private readonly HttpClient client = null;
        //private readonly IVnPayService _vnPayService;
        private string AuthenAPI_URL = "";
        private ChildrenEnglishGameLibrary methcall = new();
        private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
        private readonly CookieOptions cookieOptions = new CookieOptions
        {
            Expires = DateTime.Now.AddMinutes(10),
            MaxAge = TimeSpan.FromMinutes(10),
            Secure = true,
            IsEssential = true,
        };
        public AuthController(ILogger<AuthController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            client = new HttpClient()
            {
                BaseAddress = new Uri(config.GetSection(Constants.SYSTEM_DEFAULT_API_HTTPS_URL_CONFIG_PATH).Value)
            };
            //_vnPayService = vnPayService;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            AuthenAPI_URL = config.GetSection(Constants.SYSTEM_DEFAULT_API_URL_CONFIG_PATH).Value;
        }
        /*[HttpGet("Register")]
        public async Task<IActionResult> Register()
        {
            string? role = HttpContext.Session.GetString(Constants.ROLE_NAME);

            if (role == null) role = Constants.GUEST;

            TempData[Constants.ROLE_NAME] = role;

            var googleLoginDetails = await methcall.GetCookie<CreateNewMember>(Request, Constants.GOOGLE_ACC_COOKIE, jsonOptions);
            if (googleLoginDetails != null)
            {
                return View(googleLoginDetails);
            }
            return View();
        }*/
        [HttpGet("Login")]
        public IActionResult Login()
        {
            string? role = HttpContext.Session.GetString(Constants.ROLE_NAME);

            if (role == null) role = "Guest";

            TempData[Constants.ROLE_NAME] = role;

            return View();
        }

        /*[HttpGet("GoogleLogin")]
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleResponse")
            };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }*/
        /*public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!result.Succeeded)
            {
                ViewBag.error = "Error while registering your new account (Failed to Login By Google)";
                return RedirectToAction("Login");
            }

            var claim = result.Principal.Identities.FirstOrDefault().Claims.Select(claim => new
            {
                claim.Issuer,
                claim.OriginalIssuer,
                claim.Type,
                claim.Value,
            });

            var code = result.Properties.Items.FirstOrDefault(t => t.Key.Equals(Constants.GOOGLE_ACCESS_TOKEN_KEY_NAME)).Value;
            if (code == null)
            {
                ViewBag.error = "Error while registering your new account (Failed to Get Login Token)";
                return RedirectToAction("Login");
            }
            var userInfo = await GoogleUtils.GetUserInfo(code);
            methcall.SetCookie(Response, Constants.GOOGLE_ACC_COOKIE, userInfo, cookieOptions, jsonOptions, 20);

            return RedirectToAction("Register");
        }*/
        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            client.DefaultRequestHeaders.Authorization = null;
            HttpContext.Session.Clear();
            TempData[Constants.ACC_TOKEN] = null;
            TempData[Constants.ROLE_NAME] = null;
            TempData[Constants.USR_ID] = null;
            SignOut();

            // If using ASP.NET Identity, you may want to sign out the user
            // Example: await SignInManager.SignOutAsync();

            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }
        [HttpPost("Authorize")]
        public async Task<IActionResult> Authorize(AuthenRequest authenRequest)
        {
            AuthenAPI_URL += "Account/Login";

            var authenResponse = await methcall.CallMethodReturnObject<AuthenResponseVM>(
                _httpClient: client,
                options: jsonOptions,
                methodName: Constants.POST_METHOD,
                url: AuthenAPI_URL,
                inputType: authenRequest,
                _logger: _logger);

            if (authenResponse == null || authenResponse.Data == null || !authenResponse.Status)
            {
                string? role = HttpContext.Session.GetString(Constants.ROLE_NAME);

                if (role == null) role = Constants.GUEST;

                TempData[Constants.ROLE_NAME] = role;
                _logger.LogInformation("Username or Password is invalid.");
                ViewBag.error = "Username or Password is invalid.";
                return View("Login");
            }
            var responseAuth = authenResponse.Data;

            if (authenResponse.Status)
            {
                HttpContext.Session.SetString(Constants.ACC_TOKEN, responseAuth.AccessToken);
                HttpContext.Session.SetString(Constants.ROLE_NAME, responseAuth.RoleName);
                HttpContext.Session.SetString(Constants.USR_ID, responseAuth.AccountId);
                HttpContext.Session.SetString(Constants.USR_NAME, responseAuth.UserName);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", responseAuth.AccessToken);

                TempData[Constants.ACC_TOKEN] = responseAuth.AccessToken;
                TempData[Constants.ROLE_NAME] = responseAuth.RoleName;
                TempData[Constants.USR_ID] = responseAuth.AccountId;
                TempData[Constants.USR_NAME] = responseAuth.UserName;
            }
            switch (responseAuth.RoleName)
            {
                case var value when value.Equals(Constants.ADMIN):
                    {
                        _logger.LogInformation("Admin Login Successful: " + TempData[Constants.ROLE_NAME] + " , Id: " + TempData[Constants.USR_ID]);
                        return base.Redirect(Constants.ADMIN_URL);
                    }
                case var value when value.Equals(Constants.TEACHER):
                    {
                        _logger.LogInformation("Teacher Login Successful: " + TempData[Constants.ROLE_NAME] + " , Id: " + TempData[Constants.USR_ID]);
                        return base.Redirect("");
                    }
                case var value when value.Equals(Constants.PARENT):
                    {
                        _logger.LogInformation("Parent Login Successful: " + TempData[Constants.ROLE_NAME] + " , Id: " + TempData[Constants.USR_ID]);
                        return base.Redirect("");
                    }
                case var value when value.Equals(Constants.STUDENT):
                    {
                        _logger.LogInformation("Student Login Successful: " + TempData[Constants.ROLE_NAME] + " , Id: " + TempData[Constants.USR_ID]);
                        return base.Redirect("");
                    }
                default:
                    {
                        _logger.LogInformation("Goofy Ahh Member Login Successful: " + TempData[Constants.ROLE_NAME] + " , Id: " + TempData[Constants.USR_ID]);
                        return base.Redirect(Constants.MEMBER_URL);
                    }
            }
        }
        /*[HttpGet("ConfirmRegister")]
        //[Authorize(Roles = "TempMember")]
        public async Task<IActionResult> ConfirmRegister()
        {
            AuthenAPI_URL += "/Register";

            string TransactionAPI_URL = "/api/Transaction/UpdateUser";

            var newmemRequest = await methcall.GetCookie<CreateNewMember>(Request, Constants.NEW_MEMBER_REGISTRATION_COOKIE, jsonOptions);

            if (newmemRequest == null)
            {
                _logger.LogInformation("Error while registering your new account ! Please Try Again");
                ViewBag.error = "Error while registering your new account ! ";
                return View("Register");
            }

            methcall.RemoveCookie(Response, Constants.NEW_MEMBER_REGISTRATION_COOKIE, cookieOptions, jsonOptions);

            var authenResponse = await methcall.CallMethodReturnObject<GetAuthenResponse>(
                _httpClient: client,
                options: jsonOptions,
                methodName: Constants.POST_METHOD,
                url: AuthenAPI_URL,
                inputType: newmemRequest,
                _logger: _logger);

            if (authenResponse == null)
            {
                _logger.LogError("Error while registering your new account");

                ViewBag.error = "Error while registering your new account !";

                return View("Register");
            }

            var responseAuth = authenResponse.Data;

            var tran = await methcall.GetCookie<TransactionViewModel>(Request, Constants.NEW_MEMBER_REGISTRATION_TRANSACTION_COOKIE, jsonOptions);

            if (tran == null)
            {
                _logger.LogError("Error while registering your new account: Your Registration Transaction not found!");

                ViewBag.error = "Error while registering your new account: Your Registration Transaction not found! " +
                    "\nPlease contact the birdclub manager for assistance with resolving this issue!";

                return View("Register");
            }
            methcall.RemoveCookie(Response, Constants.NEW_MEMBER_REGISTRATION_TRANSACTION_COOKIE, cookieOptions, jsonOptions);

            UpdateTransactionRequest unmtr = new UpdateTransactionRequest()
            {
                MemberId = responseAuth.UserId,
                TransactionId = tran.TransactionId
            };

            string? accToken = HttpContext.Session.GetString(Constants.ACC_TOKEN);

            var transactionResponse = await methcall.CallMethodReturnObject<GetTransactionResponse>(
                _httpClient: client,
                options: jsonOptions,
                methodName: Constants.PUT_METHOD,
                url: TransactionAPI_URL,
                inputType: unmtr,
                accessToken: accToken,
                _logger: _logger);

            if (transactionResponse == null)
            {
                _logger.LogError("Error while registering your new account: User Transaction Saving Failed!");

                ViewBag.error = "Error while registering your new account: User Transaction Saving Failed!, " +
                    "\nPlease contact the birdclub manager for assistance with resolving this issue!";

                return View("Register");
            }
            if (authenResponse.Status)
            {
                HttpContext.Session.Remove(Constants.ACC_TOKEN);
                HttpContext.Session.Remove(Constants.USR_NAME);
                HttpContext.Session.Remove(Constants.ROLE_NAME);
            }
            ViewBag.Success = "Account Create Successfully, Please contact the manager for your account approval!";

            NotificationViewModel notif = new NotificationViewModel()
            {
                NotificationId = Guid.NewGuid().ToString(),
                Title = Constants.NOTIFICATION_TYPE_ACCOUNT_REGISTER,
                Description = Constants.NOTIFICATION_DESCRIPTION_ACCOUNT_REGISTER,
                Date = DateTime.Now,
                UserId = transactionResponse.Data.UserId,
                Status = Constants.NOTIFICATION_STATUS_UNREAD
            };
            string NotificationAPI_URL = "/api/Notification/CreateRegister";

            var notificationResponse = await methcall.CallMethodReturnObject<GetNotificationPostResponse>(
                    _httpClient: client,
                    options: jsonOptions,
                    methodName: Constants.POST_METHOD,
                    url: NotificationAPI_URL,
                    inputType: notif,
                    accessToken: accToken,
                    _logger: _logger);

            if (notificationResponse == null)
            {
                ViewBag.Error =
                    "Error while processing your request! (Create Notification).\n User Not Found!";
                return RedirectToAction("Login", "Auth");
            }
            if (!notificationResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + notificationResponse.Status + " , Error Message: " + notificationResponse.ErrorMessage);
                ViewBag.Error =
                    "Error while processing your request! (Create Notification!).\n"
                    + notificationResponse.ErrorMessage;
                return RedirectToAction("Login", "Auth");
            }
            return RedirectToAction("Login", "Auth");
        }*/
        /*[HttpPost("Register")]
        public async Task<IActionResult> RegisterMember(CreateNewMember newmemRequest)
        {
            AuthenAPI_URL += "/RegisterTempMember";

            if (!TryValidateModel(newmemRequest))
            {
                ViewBag.Error =
                "Error while processing your request! (Registering New Member!).\n Validation Failed!";
                return View("Register");
            }
            var authenResponse = await methcall.CallMethodReturnObject<GetAuthenResponse>(
                _httpClient: client,
                options: jsonOptions,
                methodName: Constants.POST_METHOD,
                url: AuthenAPI_URL,
                inputType: newmemRequest,
                _logger: _logger);

            if (authenResponse == null)
            {
                _logger.LogInformation("Error while registering your new account: ");
                ViewBag.error = "Error while registering your new account ! ";
                return View("Register");
            }
            var responseAuth = authenResponse.Data;

            if (authenResponse.Status)
            {
                HttpContext.Session.SetString(Constants.ACC_TOKEN, responseAuth.AccessToken);
                HttpContext.Session.SetString(Constants.ROLE_NAME, responseAuth.RoleName);
                HttpContext.Session.SetString(Constants.USR_NAME, responseAuth.UserName);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", responseAuth.AccessToken);
            }

            methcall.SetCookie(Response, Constants.NEW_MEMBER_REGISTRATION_COOKIE, newmemRequest, cookieOptions, jsonOptions, 20);
            PaymentInformationModel model = new PaymentInformationModel()
            {
                Fullname = newmemRequest.FullName,
                PayAmount = newmemRequest.PayAmount,
                TransactionType = Constants.NEW_MEMBER_REGISTRATION_TRANSACTION_TYPE,
            };
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);
            return Redirect(url);
        }*/
    }
}
