using CEG_BAL.Configurations;
using CEG_BAL.Services.Implements;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels.Admin.Update;
using CEG_BAL.ViewModels.Admin;
using CEG_BAL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CEG_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IClassService _classService;
        private readonly IScheduleService _scheduleService;
        private readonly ISessionService _sessionService;
        private readonly IConfiguration _config;

        public ScheduleController(IClassService classService, IScheduleService scheduleService, ISessionService sessionService, IConfiguration config)
        {
            _classService = classService;
            _scheduleService = scheduleService;
            _sessionService = sessionService;
            _config = config;
        }

        [HttpGet("All")]
        [ProducesResponseType(typeof(List<ScheduleViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetScheduleList()
        {
            try
            {
                var result = await _scheduleService.GetList();
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Schedule List Not Found!"
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
        [ProducesResponseType(typeof(List<ScheduleViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetScheduleListAdmin()
        {
            try
            {
                var result = await _scheduleService.GetListAdmin();
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Schedule List Not Found!"
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
        [ProducesResponseType(typeof(ScheduleViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetScheduleById([FromRoute] int id)
        {
            try
            {
                var result = await _scheduleService.GetById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Schedule Not Found!"
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
        //[HttpGet("Admin/{id}")]
        //[ProducesResponseType(typeof(ScheduleViewModel), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> GetClassByIdAdmin([FromRoute] int id)
        //{
        //    try
        //    {
        //        var result = await _classService.GetByIdAdmin(id);
        //        if (result == null)
        //        {
        //            return NotFound(new
        //            {
        //                Status = false,
        //                ErrorMessage = "Class Not Found!"
        //            });
        //        }
        //        return Ok(new
        //        {
        //            Status = true,
        //            Data = result
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new
        //        {
        //            Status = false,
        //            ErrorMessage = ex.Message,
        //            InnerExceptionMessage = ex.InnerException?.Message
        //        });
        //    }
        //}
        [HttpPut("{id}/Update/Status")]
        [Authorize(Roles = "Teacher,Admin")]
        [ProducesResponseType(typeof(ScheduleViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateStatus(
            [FromRoute][Required] int id,
            [FromBody][Required] string status
            )
        {
            try
            {
                var result = await _scheduleService.GetById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Schedule Does Not Exist!"
                    });
                }
                bool isValid = CEG_BAL_Library.IsScheduleNewStatusValid(result.Status, status);
                if (isValid)
                {
                    _scheduleService.UpdateStatus(id, status);
                    result = await _scheduleService.GetById(id);
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
                        ErrorMessage = "New status is either an old status or not a valid status for requested schedule"
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
        [ProducesResponseType(typeof(ScheduleViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(
            [FromRoute][Required] int id,
            [FromBody][Required] UpdateSchedule scheduleVM
            )
        {
            try
            {
                var result = await _scheduleService.GetById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Schedule Does Not Exist!"
                    });
                }
                _scheduleService.Update(result, scheduleVM);
                result = await _scheduleService.GetById(id);
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
        [ProducesResponseType(typeof(ScheduleViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSchedule(
            [FromBody][Required] CreateNewSchedule newSchedule
            )
        {
            try
            {
                var resultClassName = await _classService.IsClassEditableById(newSchedule.ClassId);
                if (!resultClassName)
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Class not found or not in Editable state."
                    });
                }
                var resultSessionName = await _sessionService.GetSessionById(newSchedule.SessionId);
                if (resultSessionName == null)
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Session not found."
                    });
                }
                ScheduleViewModel clas = new ScheduleViewModel();
                _scheduleService.Create(clas, newSchedule);
                return Ok(new
                {
                    Data = true,
                    Status = true,
                    SuccessMessage = "Schedule create successfully."
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
