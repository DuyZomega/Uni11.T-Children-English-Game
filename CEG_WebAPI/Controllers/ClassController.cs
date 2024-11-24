using CEG_BAL.Configurations;
using CEG_BAL.Services.Implements;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Admin;
using CEG_BAL.ViewModels.Admin.Get;
using CEG_BAL.ViewModels.Admin.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace CEG_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IClassService _classService;
        private readonly ICourseService _courseService;
        private readonly ITeacherService _teacherService;
        private readonly IConfiguration _config;

        public ClassController(IClassService classService, ICourseService courseService, ITeacherService teacherService, IConfiguration config)
        {
            _classService = classService;
            _courseService = courseService;
            _teacherService = teacherService;
            _config = config;
        }

        [HttpGet("All")]
        [ProducesResponseType(typeof(List<ClassViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetClassList()
        {
            try
            {
                var result = await _classService.GetClassList();
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Class List Not Found!"
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

        [HttpGet("Admin/All")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(List<ClassViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetClassListAdmin()
        {
            try
            {
                var result = await _classService.GetListAdmin();
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Class List Not Found!"
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

        [HttpGet("All/Status/Open")]
        [ProducesResponseType(typeof(List<GetClassForTransaction>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetClassOptionListByStatusOpen()
        {
            try
            {
                var result = await _classService.GetClassOptionListByStatusOpen();
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Class option list by status Open not found!"
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

        [HttpGet("Parent/All")]
        [Authorize(Roles = "Parent")]
        [ProducesResponseType(typeof(List<ClassViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetClassListParent()
        {
            try
            {
                var result = await _classService.GetClassListParent();
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Class List Not Found!"
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
        [HttpGet("{id}/All")]
        [Authorize(Roles = "Teacher")]
        [ProducesResponseType(typeof(List<ClassViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetClassListTeacher([FromRoute] int id)
        {
            try
            {
                var result = await _classService.GetClassListByTeacherAccountId(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Class List Not Found!"
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
        [ProducesResponseType(typeof(ClassViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetClassById([FromRoute] int id)
        {
            try
            {
                var result = await _classService.GetClassById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Class Not Found!"
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
        [HttpGet("Admin/{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ClassViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetClassByIdAdmin([FromRoute] int id)
        {
            try
            {
                var result = await _classService.GetByIdAdmin(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Class Not Found!"
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

        [HttpGet("Parent/{id}")]
        [Authorize(Roles = "Parent")]
        [ProducesResponseType(typeof(ClassViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetClassByIdParent([FromRoute] int id)
        {
            try
            {
                var result = await _classService.GetByIdParent(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Class Not Found!"
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

        [HttpPut("{id}/Update/Status")]
        [Authorize(Roles = "Teacher,Admin")]
        [ProducesResponseType(typeof(ClassViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateStatus(
            [FromRoute][Required] int id,
            [FromBody][Required] string status
            )
        {
            try
            {
                var result = await _classService.GetClassById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Class Does Not Exist!"
                    });
                }
                bool isValid = CEG_BAL_Library.IsClassNewStatusValid(result.Status, status);
                if (isValid)
                {
                    _classService.UpdateStatus(id, status);
                    result = await _classService.GetClassById(id);
                    return Ok(new
                    {
                        Status = true,
                        Data = result
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "New status is either an old status or not a valid status for requested class"
                    });
                }
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
        [HttpPut("{id}/Update")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ClassViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(
            [FromRoute][Required] int id,
            [FromBody][Required] UpdateClass classVM
            )
        {
            try
            {
                var result = await _classService.GetClassById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Class Does Not Exist!"
                    });
                }
                _classService.Update(result,classVM);
                result = await _classService.GetClassById(id);
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
        [HttpPost("Create")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ClassViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateClass(
            [FromBody][Required] CreateNewClass newClass
            )
        {
            try
            {
                var resultCourseName = await _courseService.IsCourseAvailableByName(newClass.CourseName);
                if (!resultCourseName)
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Course not found or course not Available."
                    });
                }
                var resultTeacherName = await _teacherService.IsTeacherExistByFullname(newClass.TeacherName);
                if (!resultTeacherName)
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Teacher not found."
                    });
                }
                /*if (newClass.WeeklySchedule == null || !CEG_BAL_Library.IsClassNewWeeklyScheduleValid(newClass.WeeklySchedule))
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Weekly schedule invalid."
                    });
                }*/
                ClassViewModel clas = new ClassViewModel();
                _classService.Create(clas, newClass);
                return Ok(new
                {
                    Data = true,
                    Status = true,
                    SuccessMessage = "Class create successfully."
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
