using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels.Admin;
using CEG_BAL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CEG_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IHomeworkQuestionService _questionService;
        private readonly IHomeworkService _homeworkService;
        private readonly IConfiguration _config;

        public QuestionController(IHomeworkQuestionService questionService, IHomeworkService homeworkService, IConfiguration config)
        {
            _questionService = questionService;
            _homeworkService = homeworkService;
            _config = config;
        }
        [HttpGet("All")]
        [ProducesResponseType(typeof(List<HomeworkQuestionViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetQuestionList()
        {
            try
            {
                var result = await _questionService.GetQuestionList();
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Question List Not Found!"
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
        [ProducesResponseType(typeof(HomeworkQuestionViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetQuestionById([FromRoute] int id)
        {
            try
            {
                var result = await _questionService.GetQuestionById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Question Not Found!"
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

        /*[HttpGet("ByCourse/{courseId}")]
        [ProducesResponseType(typeof(List<HomeworkQuestionViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetQuestionListByCourseId([FromRoute] int courseId)
        {
            try
            {
                var result = await _questionService.GetQuestionListById(courseId);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "No Questions Found With This Course!"
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
        }*/

        [HttpPost("Create")]
        [ProducesResponseType(typeof(HomeworkQuestionViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateQuestion(
            [FromBody][Required] CreateNewQuestion newSes
            )
        {
            try
            {
                var resulthomeworkName = await _homeworkService.IsHomeworkExistByTitle(newSes.HomeworkTitle);
                if (!resulthomeworkName)
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Homework Not Found!"
                    });
                }
                HomeworkQuestionViewModel sess = new HomeworkQuestionViewModel();
                _questionService.Create(sess, newSes);
                return Ok(new
                {
                    Data = true,
                    Status = true,
                    SuccessMessage = "Question Create Successfully!"
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
        [ProducesResponseType(typeof(HomeworkQuestionViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(
            [FromRoute][Required] int id,
            [FromBody][Required] HomeworkQuestionViewModel session
            )
        {
            try
            {
                var result = await _questionService.GetQuestionById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Question Does Not Exist"
                    });
                }
                session.HomeworkQuestionId = id;
                _questionService.Update(session);
                result = await _questionService.GetQuestionById(session.HomeworkQuestionId.Value);
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
