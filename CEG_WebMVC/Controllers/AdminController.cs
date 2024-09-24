using AutoMapper;
using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Account.Create;
using CEG_BAL.ViewModels.Admin;
using CEG_WebMVC.Libraries;
using CEG_WebMVC.Models.ViewModels.Account.Create;
using CEG_WebMVC.Models.ViewModels.Account.Get;
using CEG_WebMVC.Models.ViewModels.Class.Create;
using CEG_WebMVC.Models.ViewModels.Class.Get;
using CEG_WebMVC.Models.ViewModels.Course.Create;
using CEG_WebMVC.Models.ViewModels.Course.Get;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Text.Json;
using CEG_WebMVC.Models.ViewModels.Course.Get;
using CEG_WebMVC.Models.ViewModels.Course.Update;
using CEG_WebMVC.Models.ViewModels.Session.Get;
using CEG_WebMVC.Models.ViewModels.Session.Create;
using CEG_WebMVC.Models.ViewModels.Admin.Response;
using CEG_WebMVC.Models.ViewModels.Admin.PageModel;
using CEG_WebMVC.Models.ViewModels.Homework.Create;
using CEG_WebMVC.Models.ViewModels.Session.Update;

namespace CEG_WebMVC.Controllers
{
    [Route("Admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
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
        private ChildrenEnglishGameLibrary methcall = new();

        public AdminController(ILogger<AdminController> logger, IConfiguration config, IMapper mapper)
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

        [HttpGet("Index")]
        public async Task<IActionResult> AdminIndex()
        {
            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.ADMIN) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.ADMIN));
            return View();
        }

        [HttpGet("Account/Index")]
        public async Task<IActionResult> AdminAccountIndex()
        {
            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.ADMIN) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.ADMIN));
            AdminAPI_URL += "Account/All";
            string? accToken = HttpContext.Session.GetString(Constants.ACC_TOKEN);

            var accountListResponse = await methcall.CallMethodReturnObject<AdminAccountListResponseVM>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.GET_METHOD,
                url: AdminAPI_URL,
                accessToken: accToken,
                _logger: _logger);

            if (accountListResponse == null)
            {
                _logger.LogError("Error while getting account list");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while getting account list !";

                return RedirectToAction("AdminIndex");
            }
            if (!accountListResponse.Status)
            {
                _logger.LogError("Error while getting account list");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while getting account list !";

                return RedirectToAction("AdminIndex");
            }
            TempData[Constants.ALERT_DEFAULT_SUCCESS_NAME] = ViewBag.Success = "Account List Get Successfully!";

            var teacherTempData = methcall.GetValidationTempData<CreateTeacherVM>(this, TempData, Constants.CREATE_TEACHER_DETAILS_VALID, "createTeacher", jsonOptions);

            AdminAccountIndexPVM pageData = new AdminAccountIndexPVM()
            {
                AccountStatuses = _mapper.Map<List<AccountStatusVM>>(accountListResponse.Data),
                createTeacher = teacherTempData != null ? teacherTempData : new CreateTeacherVM()
            };

            return View(pageData);
        }
        [HttpGet("Class/Index")]
        public async Task<IActionResult> AdminClassIndex()
        {
            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.ADMIN) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.ADMIN));
            AdminAPI_URL += "Class/All";
            string? accToken = HttpContext.Session.GetString(Constants.ACC_TOKEN);

            var courseListResponse = await methcall.CallMethodReturnObject<AdminClassListResponseVM>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.GET_METHOD,
                url: AdminAPI_URL,
                accessToken: accToken,
                _logger: _logger);

            if (courseListResponse == null)
            {
                _logger.LogError("Error while getting class list");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while getting class list !";

                return RedirectToAction("AdminIndex");
            }
            if (!courseListResponse.Status)
            {
                _logger.LogError("Error while getting class list");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while getting class list !";

                return RedirectToAction("AdminIndex");
            }
            TempData[Constants.ALERT_DEFAULT_SUCCESS_NAME] = ViewBag.Success = "Class List Get Successfully!";

            //var teacherTempData = methcall.GetValidationTempData<CreateTeacherVM>(this, TempData, Constants.CREATE_TEACHER_DETAILS_VALID, "createTeacher", jsonOptions);

            AdminClassIndexPVM pageData = new AdminClassIndexPVM()
            {
                Classes = _mapper.Map<List<IndexClassInfoVM>>(courseListResponse.Data),
                CreateClass = new CreateClassVM()
            };
            return View(pageData);
        }
        [HttpGet("Course/Index")]
        public async Task<IActionResult> AdminCourseIndex()
        {
            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.ADMIN) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.ADMIN));
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

                return RedirectToAction("AdminIndex");
            }
            if (!courseListResponse.Status)
            {
                _logger.LogError("Error while getting course list");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while getting course list !";

                return RedirectToAction("AdminIndex");
            }
            TempData[Constants.ALERT_DEFAULT_SUCCESS_NAME] = ViewBag.Success = "Course List Get Successfully!";

            var courseTempData = methcall.GetValidationTempData<CreateCourseVM>(this, TempData, Constants.CREATE_COURSE_DETAILS_VALID, "createCourse", jsonOptions);

            AdminCourseIndexPVM pageData = new AdminCourseIndexPVM()
            {
                Courses = _mapper.Map<List<IndexCourseInfoVM>>(courseListResponse.Data),
                CreateCourse = courseTempData != null ? courseTempData : new CreateCourseVM()
            };

            return View(pageData);
        }
        [HttpPost("Course/Create")]
        public async Task<IActionResult> AdminCourseCreate(
            [FromForm][Required] CreateCourseVM createCourse)
        {
            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.ADMIN) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.ADMIN));
            AdminAPI_URL += "Course/Create";
            string? accToken = HttpContext.Session.GetString(Constants.ACC_TOKEN);

            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.CREATE_COURSE_DETAILS_VALID, createCourse, jsonOptions);
                return RedirectToAction("AdminCourseIndex");
            }

            var authenResponse = await methcall.CallMethodReturnObject<AdminCourseCreateResponseVM>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.POST_METHOD,
                url: AdminAPI_URL,
                inputType: _mapper.Map<CreateNewCourse>(createCourse),
                accessToken: accToken,
                _logger: _logger);

            if (authenResponse == null)
            {
                _logger.LogError("Error while registering Course account");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while registering Course account !";

                return RedirectToAction("AdminCourseIndex");
            }
            if (!authenResponse.Status)
            {
                _logger.LogError("Error while registering Course account");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while registering Course account !";

                return RedirectToAction("AdminCourseIndex");
            }
            TempData[Constants.ALERT_DEFAULT_SUCCESS_NAME] = ViewBag.Success = "Course Create Successfully!";
            return RedirectToAction("AdminCourseIndex");
        }
        [HttpGet("Course/{courseId}")]
        public async Task<IActionResult> AdminCourseInfo(
            [FromRoute][Required] int courseId)
        {
            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.ADMIN) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.ADMIN));
            AdminAPI_URL += "Course/" + courseId;
            string? accToken = HttpContext.Session.GetString(Constants.ACC_TOKEN);

            var courseInfoResponse = await methcall.CallMethodReturnObject<AdminCourseInfoResponseVM>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.GET_METHOD,
                url: AdminAPI_URL,
                accessToken: accToken,
                _logger: _logger);

            if (courseInfoResponse == null)
            {
                _logger.LogError("Error while getting course info");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while getting course info !";

                return RedirectToAction("AdminCourseIndex");
            }
            if (!courseInfoResponse.Status || courseInfoResponse.Data == null)
            {
                _logger.LogError("Error while getting course info");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while getting course info !";

                return RedirectToAction("AdminCourseIndex");
            }
            TempData[Constants.ALERT_DEFAULT_SUCCESS_NAME] = ViewBag.Success = "Course Info Get Successfully!";

            var createSessionFailed = methcall.GetValidationTempData<CreateSessionVM>(this, TempData, Constants.CREATE_SESSION_DETAILS_VALID, "createSession", jsonOptions);
            var createHomeworkFailed = methcall.GetValidationTempData<CreateHomeworkVM>(this, TempData, Constants.CREATE_HOMEWORK_DETAILS_VALID, "createHomework", jsonOptions);
            var updateCourseFailed = methcall.GetValidationTempData<UpdateCourseVM>(this, TempData, Constants.UPDATE_COURSE_DETAILS_VALID, "updateCourse", jsonOptions);
            var updateSessionFailed = methcall.GetValidationTempData<UpdateSessionVM>(this, TempData, Constants.UPDATE_SESSION_DETAILS_VALID, "updateSession", jsonOptions);
            var sessionList = new List<AdminSessionInfoPVM>();

            if(courseInfoResponse.Data.Sessions != null && courseInfoResponse.Data.Sessions.Count > 0)
            foreach (var session in courseInfoResponse.Data.Sessions)
            {
                sessionList.Add(new AdminSessionInfoPVM(
                    courseId,
                    _mapper.Map<SessionInfoVM>(session),
                    updateSessionFailed != null && updateSessionFailed.SessionId.Equals(session.SessionId) ? updateSessionFailed : _mapper.Map<UpdateSessionVM>(session),
                    createHomeworkFailed
                    )
                );
            }

            var pageData = new AdminCourseInfoPVM(
                _mapper.Map<CourseInfoVM>(courseInfoResponse.Data),
                updateCourseFailed ?? _mapper.Map<UpdateCourseVM>(courseInfoResponse.Data),
                sessionList,
                createSessionFailed
            );

            return View(pageData);
        }
        [HttpPost("Course/{courseId}/Update")]
        public async Task<IActionResult> AdminCourseUpdate(
            [FromRoute][Required] int courseId,
            [FromForm][Required] UpdateCourseVM updateCourse )
        {
            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.ADMIN) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.ADMIN));
            AdminAPI_URL += "Course/" + courseId + "/Update";
            string? accToken = HttpContext.Session.GetString(Constants.ACC_TOKEN);
            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.UPDATE_COURSE_DETAILS_VALID, updateCourse, jsonOptions);
                return RedirectToAction("AdminCourseInfo", new { courseId });
            }
            var courseInfoResponse = await methcall.CallMethodReturnObject<AdminCourseUpdateResponseVM>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.PUT_METHOD,
                url: AdminAPI_URL,
                accessToken: accToken,
                inputType: _mapper.Map<CourseViewModel>(updateCourse),
                _logger: _logger);

            if (courseInfoResponse == null)
            {
                _logger.LogError("Error while getting course info");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while getting course info !";

                return RedirectToAction("AdminCourseInfo", new { courseId });
            }
            if (!courseInfoResponse.Status)
            {
                _logger.LogError("Error while getting course info");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while getting course info !";

                return RedirectToAction("AdminCourseInfo", new { courseId });
            }
            TempData[Constants.ALERT_DEFAULT_SUCCESS_NAME] = ViewBag.Success = "Course Info Update Successfully!";

            return RedirectToAction("AdminCourseInfo", new { courseId });
        }
        [HttpPost("Course/{courseId}/Session/Create")]
        public async Task<IActionResult> AdminSessionCreate(
            [FromRoute][Required] int courseId,
            [FromForm][Required] CreateSessionVM createSession)
        {
            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.ADMIN) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.ADMIN));
            AdminAPI_URL += "Session/Create";
            string? accToken = HttpContext.Session.GetString(Constants.ACC_TOKEN);

            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.CREATE_SESSION_DETAILS_VALID, createSession, jsonOptions);
                return RedirectToAction("AdminCourseInfo", new { courseId });
            }

            var authenResponse = await methcall.CallMethodReturnObject<AdminSessionCreateResponseVM>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.POST_METHOD,
                url: AdminAPI_URL,
                inputType: _mapper.Map<CreateNewSession>(createSession),
                accessToken: accToken,
                _logger: _logger);

            if (authenResponse == null)
            {
                _logger.LogError("Error while registering Session account");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while registering Session account !";

                return RedirectToAction("AdminCourseInfo", new { courseId });
            }
            if (!authenResponse.Status)
            {
                _logger.LogError("Error while registering Session account");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while registering Session account !";

                return RedirectToAction("AdminCourseInfo", new { courseId });
            }
            TempData[Constants.ALERT_DEFAULT_SUCCESS_NAME] = ViewBag.Success = "Session Create Successfully!";
            return RedirectToAction("AdminCourseInfo", new { courseId });
        }
        [HttpPost("Course/{courseId}/Session/{sessionId}/Update")]
        public async Task<IActionResult> AdminSessionUpdate(
            [FromRoute][Required] int courseId,
            [FromRoute][Required] int sessionId,
            [FromForm][Required] UpdateSessionVM updateSession)
        {
            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.ADMIN) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.ADMIN));
            AdminAPI_URL += "Session/" + sessionId + "/Update";
            string? accToken = HttpContext.Session.GetString(Constants.ACC_TOKEN);
            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.UPDATE_SESSION_DETAILS_VALID, updateSession, jsonOptions);
                return RedirectToAction("AdminCourseInfo", new { courseId });
            }
            var courseInfoResponse = await methcall.CallMethodReturnObject<AdminSessionUpdateResponseVM>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.PUT_METHOD,
                url: AdminAPI_URL,
                accessToken: accToken,
                inputType: _mapper.Map<SessionViewModel>(updateSession),
                _logger: _logger);

            if (courseInfoResponse == null)
            {
                _logger.LogError("Error while getting session info");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while getting session info !";

                return RedirectToAction("AdminCourseInfo", new { courseId });
            }
            if (!courseInfoResponse.Status)
            {
                _logger.LogError("Error while getting session info");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while getting session info !";

                return RedirectToAction("AdminCourseInfo", new { courseId });
            }
            TempData[Constants.ALERT_DEFAULT_SUCCESS_NAME] = ViewBag.Success = "Session Info Update Successfully!";

            return RedirectToAction("AdminCourseInfo", new { courseId });
        }
        [HttpPost("Course/{courseId}/Session/{sessionId}/Homework/Create")]
        public async Task<IActionResult> AdminHomeworkCreate(
            [FromRoute][Required] int courseId,
            [FromRoute][Required] int sessionId,
            [FromForm][Required] CreateHomeworkVM createHomework)
        {
            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.ADMIN) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.ADMIN));
            AdminAPI_URL += "Homework/Create";
            string? accToken = HttpContext.Session.GetString(Constants.ACC_TOKEN);

            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.CREATE_HOMEWORK_DETAILS_VALID, createHomework, jsonOptions);
                return RedirectToAction("AdminCourseInfo", new { courseId });
            }

            var authenResponse = await methcall.CallMethodReturnObject<AdminHomeworkCreateResponseVM>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.POST_METHOD,
                url: AdminAPI_URL,
                inputType: _mapper.Map<CreateNewHomework>(createHomework),
                accessToken: accToken,
                _logger: _logger);

            if (authenResponse == null)
            {
                _logger.LogError("Error while registering Homework account");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while registering Homework account !";

                return RedirectToAction("AdminCourseInfo", new { courseId });
            }
            if (!authenResponse.Status)
            {
                _logger.LogError("Error while registering Homework account");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while registering Homework account !";

                return RedirectToAction("AdminCourseInfo", new { courseId });
            }
            TempData[Constants.ALERT_DEFAULT_SUCCESS_NAME] = ViewBag.Success = "Homework Create Successfully!";
            return RedirectToAction("AdminCourseInfo", new { courseId });
        }
        [HttpGet("Transaction/Index")]
        public async Task<IActionResult> AdminTransactionIndex()
        {
            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.ADMIN) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.ADMIN));
            return View();
        }
        [HttpPost("Account/Create/Teacher")]
        //[Authorize(Roles = "TempMember")]
        public async Task<IActionResult> AdminTeacherCreate(
            [FromForm][Required] CreateTeacherVM createTeacher)
        {

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.ADMIN) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.ADMIN));
            AdminAPI_URL += "Admin/Teacher/Create";
            string? accToken = HttpContext.Session.GetString(Constants.ACC_TOKEN);

            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.CREATE_TEACHER_DETAILS_VALID, createTeacher, jsonOptions);
                return RedirectToAction("AdminAccountIndex");
            }

            var teacherAccountCreateResponse = await methcall.CallMethodReturnObject<AdminAccountCreateResponseVM>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.POST_METHOD,
                url: AdminAPI_URL,
                inputType: _mapper.Map<CreateNewTeacher>(createTeacher),
                accessToken: accToken,
                _logger: _logger);

            if (teacherAccountCreateResponse == null)
            {
                _logger.LogError("Error while registering Teacher account");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while registering Teacher account !";

                return RedirectToAction("AdminAccountIndex");
            }
            if (!teacherAccountCreateResponse.Status)
            {
                _logger.LogError("Error while registering Teacher account");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while registering Teacher account !";

                return RedirectToAction("AdminAccountIndex");
            }
            TempData[Constants.ALERT_DEFAULT_SUCCESS_NAME] = ViewBag.Success = "Teacher Account Create Successfully!";

            return RedirectToAction("AdminAccountIndex");
        }
        [HttpPost("Account/Create/Parent")]
        //[Authorize(Roles = "TempMember")]
        public async Task<IActionResult> AdminParentCreate(
            [FromForm][Required] CreateParentVM createParent)
        {

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.ADMIN) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.ADMIN));
            AdminAPI_URL += "Admin/Parent/Create";
            string? accToken = HttpContext.Session.GetString(Constants.ACC_TOKEN);

            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.CREATE_PARENT_DETAILS_VALID, createParent, jsonOptions);
                return RedirectToAction("AdminAccountIndex");
            }

            var parentAccountCreateResponse = await methcall.CallMethodReturnObject<AdminAccountCreateResponseVM>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.POST_METHOD,
                url: AdminAPI_URL,
                inputType: _mapper.Map<CreateNewParent>(createParent),
                accessToken: accToken,
                _logger: _logger);

            if (parentAccountCreateResponse == null)
            {
                _logger.LogError("Error while registering Parent account");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while registering Parent account !";

                return RedirectToAction("AdminAccountIndex");
            }
            if (!parentAccountCreateResponse.Status)
            {
                _logger.LogError("Error while registering Parent account");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while registering Parent account !";

                return RedirectToAction("AdminAccountIndex");
            }
            TempData[Constants.ALERT_DEFAULT_SUCCESS_NAME] = ViewBag.Success = "Parent Account Create Successfully!";

            return RedirectToAction("AdminAccountIndex");
        }
        [HttpPost("Account/Create/Student")]
        //[Authorize(Roles = "TempMember")]
        public async Task<IActionResult> AdminStudentCreate(
            [FromForm][Required] CreateStudentVM createStudent)
        {

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.ADMIN) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.ADMIN));
            AdminAPI_URL += "Admin/Student/Create";
            string? accToken = HttpContext.Session.GetString(Constants.ACC_TOKEN);

            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.CREATE_STUDENT_DETAILS_VALID, createStudent, jsonOptions);
                return RedirectToAction("AdminAccountIndex");
            }

            var studentAccountCreateResponse = await methcall.CallMethodReturnObject<AdminAccountCreateResponseVM>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.POST_METHOD,
                url: AdminAPI_URL,
                inputType: _mapper.Map<CreateNewStudent>(createStudent),
                accessToken: accToken,
                _logger: _logger);

            if (studentAccountCreateResponse == null)
            {
                _logger.LogError("Error while registering Student account");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while registering Student account !";

                return RedirectToAction("AdminAccountIndex");
            }
            if (!studentAccountCreateResponse.Status)
            {
                _logger.LogError("Error while registering Student account");

                TempData[Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while registering Student account !";

                return RedirectToAction("AdminAccountIndex");
            }
            TempData[Constants.ALERT_DEFAULT_SUCCESS_NAME] = ViewBag.Success = "Student Account Create Successfully!";

            return RedirectToAction("AdminAccountIndex");
        }
    }
}
