using CEG_BAL.Configurations;
using CEG_BAL.Services.Implements;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Admin.Create;
using CEG_BAL.ViewModels.Admin.Get;
using CEG_BAL.ViewModels.Admin.Update;
using CEG_WebAPI.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CEG_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        private readonly IStudentService _studentService;
        private readonly IHomeworkService _homeworkService;
        private readonly IScheduleService _scheduleService;
        private readonly IStudentHomeworkService _studentHomeworkService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _config;

        public TeacherController(
            ITeacherService teacherService, IConfiguration config, IEmailService emailService, IStudentService studentService, IHomeworkService homeworkService, IStudentHomeworkService studentHomeworkService, IScheduleService scheduleService)
        {
            _teacherService = teacherService;
            _config = config;
            _emailService = emailService;
            _studentService = studentService;
            _homeworkService = homeworkService;
            _studentHomeworkService = studentHomeworkService;
            _scheduleService = scheduleService;
        }

        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpGet("All")]
        [ProducesResponseType(typeof(List<TeacherViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTeacherList()
        {
            return await GetList();
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpGet("All/FullnameOption")]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(List<GetTeacherNameOption>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTeacherNameOptionList()
        {
            return await GetNameOptionList();
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpGet("Student/Attendance/All/ByScheduleId/{scheduleId}")]
        [Authorize(Roles = Roles.Teacher)]
        [ProducesResponseType(typeof(AttendanceViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAttendanceByScheduleId(
            [FromRoute][Required] int scheduleId
            )
        {
            return await GetAttendanceListByScheduleId(scheduleId);
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpPut("Account/{id}/Update")]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(TeacherViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTeacher(
            [FromRoute][Required] int id,
            [FromBody][Required] UpdateTeacher teacher)
        {
            return await Update(id, teacher);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<TeacherViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var result = await _teacherService.GetTeacherList();
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Teacher List Not Found!"
                    });
                }
                return Ok(new
                {
                    Status = true,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = ex.Message,
                    InnerExceptionMessage = ex.InnerException?.Message
                });
            }
        }

        [HttpGet("fullnameoption")]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(List<GetTeacherNameOption>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetNameOptionList()
        {
            try
            {
                var result = await _teacherService.GetTeacherNameOptionList();
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Teacher name option list not found."
                    });
                }
                return Ok(new
                {
                    Status = true,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = ex.Message,
                    InnerExceptionMessage = ex.InnerException?.Message
                });
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TeacherViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(
            [FromRoute][Required] int id
            )
        {
            try
            {
                var result = await _teacherService.GetTeacherById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Teacher Not Found!"
                    });
                }
                return Ok(new
                {
                    Status = true,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = ex.Message,
                    InnerExceptionMessage = ex.InnerException?.Message
                });
            }
        }

        [HttpGet("account/{id}")]
        [Authorize(Roles = $"{Roles.Teacher}, {Roles.Admin}")]
        [ProducesResponseType(typeof(TeacherViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByAccountId(
            [FromRoute][Required] int id
            )
        {
            try
            {
                var result = await _teacherService.GetByAccountId(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Teacher Not Found!"
                    });
                }
                return Ok(new
                {
                    Status = true,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = ex.Message,
                    InnerExceptionMessage = ex.InnerException?.Message
                });
            }
        }

        [HttpGet("student/attendance/schedule/{scheduleId}")]
        [Authorize(Roles = Roles.Teacher)]
        [ProducesResponseType(typeof(AttendanceViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAttendanceListByScheduleId(
            [FromRoute][Required] int scheduleId
            )
        {
            try
            {
                var result = await _teacherService.GetStudentActivityListByScheduleId(scheduleId);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Attendance List Not Found!"
                    });
                }
                return Ok(new
                {
                    Status = true,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = ex.Message,
                    InnerExceptionMessage = ex.InnerException?.Message
                });
            }
        }

        [HttpPost("sendremindhomeworkemail")]
        [Authorize(Roles = Roles.Teacher)]
        [ProducesResponseType(typeof(AttendanceViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SendEmailRemindHomeworktoParent(
            [FromBody][Required] CreateRemindHomeworkEmail emaReq
            )
        {
            try
            {
                var stu = await _studentService.GetStudentById(emaReq.StudentId);
                if (stu == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Student Not Found!"
                    });
                }
                var hom = await _homeworkService.GetHomeworkById(emaReq.HomeworkId);
                if (hom == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Homework Not Found!"
                    });
                }
                var sch = await _scheduleService.GetById(emaReq.ScheduleId);
                if (sch == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Schedule Not Found!"
                    });
                }
                var stuHomList = await _studentHomeworkService.GetListByAccountId(emaReq.StudentId);
                if (stuHomList == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Student homework not found."
                    });
                }
                if (stuHomList != null &&
                    stuHomList.Any(stuHom =>
                        stuHom.StudentProgress.StudentId == emaReq.StudentId &&
                        stuHom.HomeworkId == emaReq.HomeworkId &&
                        stuHom.Status == CEGConstants.STUDENT_HOMEWORK_STATUS_SUBMITTED
                        )
                    )
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Student has already submitted their homework."
                    });
                }

                await _emailService.SendEmailAsync(
                    _fromSenderName: _config.GetSection("Gmail:SenderName").Value,
                    _fromEmail: _config.GetSection("Gmail:Username").Value,
                    stu.Parent.Account.Fullname,
                    stu.Parent.Email,
                    "CEG Warning: Unsubmitted Homework!",
                    "<h2 style=\"color: red;\">Warning: Unsubmitted Homework!</h2>" +
                    "<p>Dear Parent,</p>" +
                    "<p>" +
                    $"    We are writing to inform you that your child, <strong>{stu.Account.Fullname}</strong>, " +
                    "       has not submitted their homework for the assignment titled " +
                    $"    <strong>{hom.Title}</strong>." +
                    $" This homework is from schedule number <strong>{sch.Session.SessionNumber}</strong>" +
                    $" of the class <strong>{sch.Class.ClassName}</strong>." +
                    "</p>" +
                    "<p>" +
                    $"    The deadline for this homework is <strong>{hom.EndDate?.ToString("dddd, MMMM dd, yyyy") ?? "N/A"}</strong>. " +
                    "Please ensure that your child completes and submits the homework before the deadline to avoid any academic penalties." +
                    "</p>" +
                    "<p>" +
                    "    Thank you for your attention to this matter." +
                    "</p>" +
                    "<p>Best regards,</p>" +
                    $"<p><strong>{_config.GetSection("Gmail:SenderName").Value}</strong></p>"
                    /*"Warning, your child have not submit his / her homework, please remind your child about your studying before homework deadline!",
                    "   <h2>Warning, your child have not submit his / her homework!</h2>" +
                    "<div>" +
                    "   <h3>Dear Parent, please remind your child about your studying before the homework deadline!</h3>" +
                    "   <h4>Student / child fullname: " + stu.Account.Fullname + "</h4>" +
                    "   <h4>Unsubmitted homework name: " + hom.Title + " , from schedule number: " + sch.ScheduleNumber +" of class: " + sch.Class.ClassName +"</h4>" +
                    "   <h4>Homework deadline: " + hom.EndDate + "</h4>" +
                    "</div>"*/
                    ,
                    _config,
                    _stmpUser: _config.GetSection("Gmail:Username").Value,
                    _stmpAppPassword: _config.GetSection("Gmail:Password").Value
                );

                return Ok(new
                {
                    Status = true,
                    Data = true
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = ex.Message,
                    InnerExceptionMessage = ex.InnerException?.Message
                });
            }
        }

        [HttpPut("account/{id}")]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(TeacherViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(
            [FromRoute][Required] int id,
            [FromBody][Required] UpdateTeacher teacher
            )
        {
            try
            {
                var result = await _teacherService.GetByAccountId(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Teacher Does Not Exist"
                    });
                }
                _teacherService.Update(result, teacher);
                result = await _teacherService.GetByAccountId(id);
                return Ok(new
                {
                    Status = true,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = ex.Message,
                    InnerExceptionMessage = ex.InnerException?.Message
                });
            }

        }
    }
}
