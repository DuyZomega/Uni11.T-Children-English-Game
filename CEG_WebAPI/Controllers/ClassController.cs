using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
                var result = await _classService.GetClassListAdmin();
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
        [HttpGet("{teacherid}/All")]
        [Authorize(Roles = "Teacher")]
        [ProducesResponseType(typeof(List<ClassViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetClassListTeacher([FromRoute] int teacherid)
        {
            try
            {
                var result = await _classService.GetClassListByTeacherId(teacherid);
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
                        ErrorMessage = "Course Not Found or Course not Available!"
                    });
                }
                var resultTeacherName = await _teacherService.IsTeacherExistByFullname(newClass.TeacherName);
                if (!resultTeacherName)
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Teacher Not Found!"
                    });
                }
                ClassViewModel clas = new ClassViewModel();
                _classService.Create(clas, newClass);
                return Ok(new
                {
                    Data = true,
                    Status = true,
                    SuccessMessage = "Class Create Successfully!"
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
