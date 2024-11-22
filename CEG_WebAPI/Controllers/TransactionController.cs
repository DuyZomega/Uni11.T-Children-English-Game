using CEG_BAL.Services.Implements;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Parent;
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

        [HttpPost("GenerateUrl")]
        [Authorize(Roles = "Admin,Parent")]
        [ProducesResponseType(typeof(TransactionRequest), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GeneratePaymentUrl(
            [FromBody][Required] TransactionRequest request
            )
        {
            try
            {
                var parentObj = await _parentService.IsParentExistByFullname(request.ParentFullname);
                if(!parentObj)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Parent not found."
                    });
                }
                var studentObj = await _studentService.GetStudentNameListByParentName(request.ParentFullname);
                if (!studentObj.Contains(request.StudentFullname))
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Student not found."
                    });
                }
                var classObj = await _classService.GetClassOptionListByStatusOpen();
                if (!classObj.Exists(clas => clas.ClassName.Equals(request.Classname)))
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Class not found or not Open for Enrollment."
                    });
                }
                var result = _vnpayService.CreatePaymentUrl(request);
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
            [Required][FromBody] CreateTransaction newTran)
        {
            try
            {
                var resultParentName = await _parentService.IsParentExistByFullname(newTran.ParentFullname);
                if (!resultParentName)
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Parent not found."
                    });
                }
                TransactionViewModel tran = new TransactionViewModel();
                var tranId = await _transactionService.Create(tran, newTran);
                if (newTran.TransactionType.Equals("Enrollment"))
                {
                    var newEn = new CreateNewEnroll()
                    {
                        StudentName = newTran.StudentFullname,
                        ClassName = newTran.ClassName,
                        TransactionId = tranId,
                    };
                    EnrollViewModel newEnroll = new EnrollViewModel();
                    _enrollService.Create(newEnroll, newEn);
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
