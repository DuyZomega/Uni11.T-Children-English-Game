﻿using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Account.Create;
using CEG_WebMVC.Library;
using CEG_WebMVC.Models.ViewModels.Account;
using CEG_WebMVC.Models.ViewModels.Admin;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
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
        private ChildrenEnglishGameLibrary methcall = new();

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
            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.ADMIN) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.ADMIN));
            return View();
        }

        [HttpGet("AccountIndex")]
        public async Task<IActionResult> AdminAccountIndex()
        {
            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.ADMIN) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.ADMIN));

            var adminAccounts = new AdminAccountIndexVM();
            return View(adminAccounts);
        }
        [HttpPost("Account/Create/Teacher")]
        //[Authorize(Roles = "TempMember")]
        public async Task<IActionResult> AdminCreateTeacher(
            [Required] CreateNewTeacher createTeacher)
        {
            AdminAPI_URL += "Admin/CreateTeacher";

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.ADMIN) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.ADMIN));
            string? accToken = HttpContext.Session.GetString(Constants.ACC_TOKEN);

            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.CREATE_EMPLOYEE_DETAILS_VALID, createTeacher, jsonOptions);
                return RedirectToAction("AdminAccountIndex");
            }
            var authenResponse = await methcall.CallMethodReturnObject<AuthenResponseVM>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.PUT_METHOD,
                url: AdminAPI_URL,
                inputType: createTeacher,
                accessToken: accToken,
                _logger: _logger);

            if (authenResponse == null)
            {
                _logger.LogError("Error while registering Teacher account");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while registering Teacher account !";

                return RedirectToAction("AdminAccountIndex");
            }
            if (authenResponse.Status)
            {
                _logger.LogError("Error while registering Teacher account");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while registering Teacher account !";

                return RedirectToAction("AdminAccountIndex");
            }
            TempData["Success"] = ViewBag.Success = "Teacher Account Create Successfully!";

            return RedirectToAction("AdminAccountIndex");
        }
    }
}
