using CEG_BAL.Services.Implements;
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
    public class HomeworkController : ControllerBase
    {
        private readonly IHomeworkService _homeworkService;
        private readonly ISessionService _sessionService;
        private readonly IConfiguration _config;

        public HomeworkController(IHomeworkService homeworkService, ISessionService sessionService, IConfiguration config)
        {
            _homeworkService = homeworkService;
            _sessionService = sessionService;
            _config = config;
        }

        [HttpGet("All")]
        [ProducesResponseType(typeof(List<HomeworkViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetHomeworkList()
        {
            try
            {
                var result = await _homeworkService.GetHomeworkList();
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Homework List Not Found!"
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
        [ProducesResponseType(typeof(HomeworkViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetHomeworkById([FromRoute] int id)
        {
            try
            {
                var result = await _homeworkService.GetHomeworkById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Homework Not Found!"
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
        [ProducesResponseType(typeof(HomeworkViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateHomework(
            [FromBody][Required] CreateNewHomework newHw
            )
        {
            try
            {
                var resultSessionTitle = await _sessionService.IsSessionExistByTitle(newHw.SessionTitle);
                if (!resultSessionTitle)
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Session Not Found!"
                    });
                }
                HomeworkViewModel hw = new HomeworkViewModel();
                _homeworkService.Create(hw, newHw);
                return Ok(new
                {
                    Data = true,
                    Status = true,
                    SuccessMessage = "Homework Create Successfully!"
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
        [HttpPut("{id}/Update")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(HomeworkViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(
            [FromRoute][Required] int id,
            [FromBody][Required] HomeworkViewModel homework
            )
        {
            try
            {
                var result = await _homeworkService.GetHomeworkById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Homework Does Not Exist"
                    });
                }
                homework.HomeworkId = id;
                _homeworkService.Update(homework);
                result = await _homeworkService.GetHomeworkById(homework.HomeworkId.Value);
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
