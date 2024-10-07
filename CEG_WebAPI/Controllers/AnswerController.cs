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
    public class AnswerController : ControllerBase
    {
        private readonly IHomeworkAnswerService _answerService;
        private readonly IHomeworkQuestionService _questionService;
        private readonly IConfiguration _config;

        public AnswerController(IHomeworkAnswerService answerService, IHomeworkQuestionService questionService, IConfiguration config)
        {
            _answerService = answerService;
            _questionService = questionService;
            _config = config;
        }

        [HttpGet("All")]
        [ProducesResponseType(typeof(List<HomeworkAnswerViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAnswerList()
        {
            try
            {
                var result = await _answerService.GetAnswerList();
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Answer List Not Found!"
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
        [ProducesResponseType(typeof(HomeworkAnswerViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAnswerById([FromRoute] int id)
        {
            try
            {
                var result = await _answerService.GetAnswerById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Answer Not Found!"
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
        [ProducesResponseType(typeof(List<HomeworkAnswerViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAnswerListByCourseId([FromRoute] int courseId)
        {
            try
            {
                var result = await _questionService.GetAnswerListById(courseId);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "No Answers Found With This Course!"
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
        [ProducesResponseType(typeof(HomeworkAnswerViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAnswer(
            [FromBody][Required] CreateNewAnswer newSes
            )
        {
            try
            {
                var resulthomeworkName = await _questionService.GetQuestionById(newSes.QuestionId.Value);
                if (resulthomeworkName == null)
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Question Not Found!"
                    });
                }
                HomeworkAnswerViewModel sess = new HomeworkAnswerViewModel();
                _answerService.Create(sess, newSes);
                return Ok(new
                {
                    Data = true,
                    Status = true,
                    SuccessMessage = "Answer Create Successfully!"
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
        [ProducesResponseType(typeof(HomeworkAnswerViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(
            [FromRoute][Required] int id,
            [FromBody][Required] HomeworkAnswerViewModel session
            )
        {
            try
            {
                var result = await _answerService.GetAnswerById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Answer Does Not Exist"
                    });
                }
                session.HomeworkAnswerId = id;
                _answerService.Update(session);
                result = await _answerService.GetAnswerById(session.HomeworkAnswerId.Value);
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
