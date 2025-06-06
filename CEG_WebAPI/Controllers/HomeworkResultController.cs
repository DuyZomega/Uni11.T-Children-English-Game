using CEG_BAL.Services.Implements;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels.Admin.Create;
using CEG_BAL.ViewModels.Admin.Update;
using CEG_BAL.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.Identity.Client;

namespace CEG_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeworkResultController : ControllerBase
    {
        private readonly IHomeworkResultService _homeworkResultService;
        private readonly IConfiguration _config;

        public HomeworkResultController(IHomeworkResultService homeworkResultService, IConfiguration config)
        {
            _homeworkResultService = homeworkResultService;
            _config = config;
        }

        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpGet("All")]
        [ProducesResponseType(typeof(List<HomeworkResultViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetStudentAnswerList()
        {
            return await GetList();
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpGet("ByStudent/{stuId}/ByHomework/{homId}")]
        [ProducesResponseType(typeof(HomeworkResultViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetHomeworkResultByStudentIdAndHomeworkId(
            [FromRoute][Required] int stuId,
            [FromRoute][Required] int homId
            )
        {
            return await GetByStudentIdAndHomeworkId(stuId, homId);
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpPost("Create")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateStudentAnswer(
            [FromBody][Required] CreateNewHomeworkResult newHomRes
            )
        {
            return await Create(newHomRes);
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpPut("{id}/Update")]
        [ProducesResponseType(typeof(HomeworkResultViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateStudentAnswer(
        [FromRoute][Required] int id,
            [FromBody][Required] UpdateHomeworkResult upHomRes
            )
        {
            return await Update(id, upHomRes);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<HomeworkResultViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var result = await _homeworkResultService.GetList();
                if (result == null || result.Count == 0)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Homework result list not found or empty!"
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
        [ProducesResponseType(typeof(HomeworkResultViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(
            [FromRoute][Required] int id
            )
        {
            try
            {
                var result = await _homeworkResultService.GetById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Homework result not found!"
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

        [HttpGet("student/{stuId}/homework/{homId}")]
        [ProducesResponseType(typeof(HomeworkResultViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByStudentIdAndHomeworkId(
            [FromRoute][Required] int stuId,
            [FromRoute][Required] int homId
            )
        {
            try
            {
                var result = await _homeworkResultService.GetByStudentIdAndHomeworkId(stuId, homId);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Homework result not found!"
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
            [FromBody][Required] CreateNewHomeworkResult newHomRes
            )
        {
            try
            {
                await _homeworkResultService.Create(newHomRes);
                return Ok(new
                {
                    Data = true,
                    Status = true,
                    SuccessMessage = "Homework result create successfully."
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
        [ProducesResponseType(typeof(HomeworkResultViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(
            [FromRoute][Required] int id,
            [FromBody][Required] UpdateHomeworkResult upHomRes
            )
        {
            try
            {
                var result = await _homeworkResultService.GetById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Homework result does not exist."
                    });
                }
                await _homeworkResultService.Update(id, upHomRes);
                result = await _homeworkResultService.GetById(id);
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
