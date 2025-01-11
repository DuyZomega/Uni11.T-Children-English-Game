using CEG_BAL.Configurations;
using CEG_BAL.Services.Implements;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Parent;
using CEG_BAL.ViewModels.Teacher.Transaction;
using CEG_BAL.ViewModels.Transaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CEG_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IVnpayService _vnpayService;
        private readonly IParentService _parentService;
        private readonly IStudentService _studentService;
        private readonly IClassService _classService;
        private readonly IEnrollService _enrollService;

        public TransactionController(
            ITransactionService transactionService,
            IVnpayService vnpayService,
            IParentService parentService,
            IEnrollService enrollService,
            IStudentService studentService,
            IClassService classService
            )
        {
            _transactionService = transactionService;
            _vnpayService = vnpayService;
            _parentService = parentService;
            _enrollService = enrollService;
            _studentService = studentService;
            _classService = classService;
        }

        [HttpGet("All")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(List<TransactionViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTransactionList()
        {
            try
            {
                var result = await _transactionService.GetTransactionList();
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Transaction List Not Found!"
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
        [ProducesResponseType(typeof(TransactionViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTransactionById([FromRoute] int id)
        {
            try
            {
                var result = await _transactionService.GetTransactionById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Transaction Not Found!"
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

        [HttpGet("All/Count")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTotalTransactionAmount()
        {
            try
            {
                var result = await _transactionService.GetTotalAmount();
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

        [HttpGet("All/Count/{id}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTotalTransactionAmountByAccountId(
            [FromRoute][Required] int id)
        {
            try
            {
                var result = await _transactionService.GetTotalAmountByAccountId(id);
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

        [HttpGet("All/ByTeacher/{id}")]
        [Authorize(Roles = "Teacher")]
        [ProducesResponseType(typeof(List<EarningViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllTransactionsByTeacherAccountId([FromRoute][Required] int id)
        {
            try
            {
                var result = await _transactionService.GetAllByTeacherAccountId(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Transaction list for teacher not found."
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

        [HttpGet("All/Sum")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetSumTransactionValue()
        {
            try
            {
                var result = await _transactionService.GetSumValue();
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

        [HttpGet("ByParent/{id}")]
        [Authorize(Roles = "Parent")]
        [ProducesResponseType(typeof(List<TransactionViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTransactionByParentAccountId(
            [FromRoute][Required] int id)
        {
            try
            {
                var result = await _transactionService.GetTransactionByParentAccountId(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Transaction list for parent not found."
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

        [HttpPost("GenerateUrl")]
        [Authorize(Roles = "Parent")]
        [ProducesResponseType(typeof(TransactionRequest), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GeneratePaymentUrl(
            [FromBody][Required] TransactionRequest newTraReq
            )
        {
            try
            {
                var checkClassFull = await _classService.CheckClassFull(newTraReq.Classname);
                if (checkClassFull)
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Class is full."
                    });
                }
                var parentObj = await _parentService.IsExistByFullname(newTraReq.ParentFullname);
                if(!parentObj)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Parent not found."
                    });
                }
                var studentObj = await _studentService.GetFullnameListByParentName(newTraReq.ParentFullname);
                if (!studentObj.Contains(newTraReq.StudentFullname))
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Student not found."
                    });
                }
                var classObj = await _classService.GetOptionListByStatusOpen();
                if (!classObj.Exists(clas => clas.ClassName.Equals(newTraReq.Classname)))
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Class not found or not Open for Enrollment."
                    });
                }
                var result = _vnpayService.CreatePaymentUrl(newTraReq);
                if (result == null)
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "An error occured when creating transaction."
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
        [Authorize(Roles = "Admin,Parent")]
        [ProducesResponseType(typeof(TransactionViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTransaction(
            [Required][FromBody] CreateTransaction newTra)
        {
            try
            {
                var checkClassFull = await _classService.CheckClassFull(newTra.ClassName);
                if (checkClassFull)
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Class is full."
                    });
                }
                var resultParentName = await _parentService.IsExistByFullname(newTra.ParentFullname);
                if (!resultParentName)
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Parent not found."
                    });
                }
                var studentObj = await _studentService.GetFullnameListByParentName(newTra.ParentFullname);
                if (!studentObj.Contains(newTra.StudentFullname))
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Student not found."
                    });
                }
                var classObj = await _classService.GetOptionListByStatusOpen();
                if (!classObj.Exists(clas => clas.ClassName.Equals(newTra.ClassName)))
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Class not found or not Open for Enrollment."
                    });
                }
                if (newTra.TransactionType.Equals(CEGConstants.TRANSACTION_TYPE_ENROLLMENT))
                {
                    var existEnr = await _enrollService.GetByStudentFullnameAndClassName(newTra.StudentFullname, newTra.ClassName);
                    if (existEnr != null)
                    {
                        return BadRequest(new
                        {
                            Status = false,
                            ErrorMessage = "Student has already enrolled to this class."
                        });
                    }
                }
                var tranId = await _transactionService.Create(newTra);
                if (newTra.TransactionType.Equals(CEGConstants.TRANSACTION_TYPE_ENROLLMENT))
                {
                    var newEn = new CreateNewEnroll()
                    {
                        StudentName = newTra.StudentFullname,
                        ClassName = newTra.ClassName,
                        TransactionId = tranId,
                    };
                    await _enrollService.Create(newEn);
                }
                return Ok(new
                {
                    Data = true,
                    Status = true,
                    SuccessMessage = "Transaction create successfully."
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
