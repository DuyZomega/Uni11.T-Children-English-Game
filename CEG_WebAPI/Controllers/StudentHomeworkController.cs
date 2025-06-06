using CEG_BAL.Services.Implements;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels.Admin.Create;
using CEG_BAL.ViewModels.Admin.Update;
using CEG_BAL.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CEG_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentHomeworkController : ControllerBase
    {
        private readonly IStudentHomeworkService _studentHomeworkService;
        private readonly IConfiguration _config;

        public StudentHomeworkController(IStudentHomeworkService studentHomeworkService, IConfiguration config)
        {
            _studentHomeworkService = studentHomeworkService;
            _config = config;
        }

        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpGet("All")]
        [ProducesResponseType(typeof(List<StudentHomeworkViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetStudentHomeworkList()
        {
            return await GetList();
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpGet("ByStudent/{stuId}")]
        [ProducesResponseType(typeof(List<StudentHomeworkViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetStudentHomeworkByStudentId(
            [FromRoute][Required] int stuId)
        {
            return await GetByStudentId(stuId);
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpPost("Create")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateStudentHomework(
            [FromBody][Required] CreateNewStudentHomework newStuHom
            )
        {
            return await Create(newStuHom);
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpPut("{id}/Update")]
        [ProducesResponseType(typeof(StudentHomeworkViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateStudentHomework(
            [FromRoute][Required] int id,
            [FromBody][Required] UpdateStudentHomework upStuHom
            )
        {
            return await Update(id, upStuHom);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<StudentHomeworkViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var result = await _studentHomeworkService.GetList();
                if (result == null || result.Count == 0)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Student homework list not found or empty!"
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
        [ProducesResponseType(typeof(StudentHomeworkViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(
            [FromRoute][Required] int id
            )
        {
            try
            {
                var result = await _studentHomeworkService.GetById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Student homework not found!"
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

        [HttpGet("student/{stuId}")]
        [ProducesResponseType(typeof(List<StudentHomeworkViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByStudentId(
            [FromRoute][Required] int stuId)
        {
            try
            {
                var result = await _studentHomeworkService.GetListByStudentId(stuId);
                if (result == null || result.Count == 0)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Student homework list not found or empty!"
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

        [HttpPost]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(
            [FromBody][Required] CreateNewStudentHomework newStuHom
            )
        {
            try
            {
                await _studentHomeworkService.Create(newStuHom);
                return Ok(new
                {
                    Data = true,
                    Status = true,
                    SuccessMessage = "Student homework create successfully."
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

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(StudentHomeworkViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(
            [FromRoute][Required] int id,
            [FromBody][Required] UpdateStudentHomework upStuHom
            )
        {
            try
            {
                var result = await _studentHomeworkService.GetById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Student homework does not exist."
                    });
                }
                await _studentHomeworkService.Update(id, upStuHom);
                result = await _studentHomeworkService.GetById(id);
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
