using CEG_BAL.Configurations;
using CEG_BAL.Services.Implements;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Parent;
using CEG_BAL.ViewModels.Teacher.Transaction;
using CEG_BAL.ViewModels.Transaction;
using CEG_WebAPI.Constants;
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

        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpGet("All")]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(List<TransactionViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTransactionList()
        {
            return await GetList();
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpGet("All/Count")]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTotalTransactionAmount()
        {
            return await GetCount();
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpGet("All/Count/{id}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTotalTransactionAmountByAccountId(
            [FromRoute][Required] int id)
        {
            return await GetAmountByAccountId(id);
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpGet("All/ByTeacher/{id}")]
        [Authorize(Roles = Roles.Teacher)]
        [ProducesResponseType(typeof(List<EarningViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllTransactionsByTeacherAccountId([FromRoute][Required] int id)
        {
            return await GetListByTeacherAccountId(id);
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpGet("All/Sum")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetSumTransactionValue()
        {
            return await GetSumValue();
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpGet("All/Sum/ByTeacher/{id}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetSumTransactionByTeacherAccountId(
            [FromRoute][Required] int id)
        {
            return await GetSumByTeacherAccountId(id);
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpGet("ByParent/{id}")]
        [Authorize(Roles = Roles.Parent)]
        [ProducesResponseType(typeof(List<TransactionViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTransactionByParentAccountId(
            [FromRoute][Required] int id)
        {
            return await GetByParentAccountId(id);
        }
        [Obsolete("This api use old Api mapping that is not correct. Use new api instead", false)]
        [HttpPost("Create")]
        [Authorize(Roles = $"{Roles.Admin}, {Roles.Parent}")]
        [ProducesResponseType(typeof(TransactionViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTransaction(
            [Required][FromBody] CreateTransaction newTra)
        {
            return await Create(newTra);
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(List<TransactionViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetList()
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
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(TransactionViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById([FromRoute] int id)
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

        [HttpGet("count")]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCount()
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

        [HttpGet("count/{id}")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Parent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAmountByAccountId([FromRoute][Required] int id)
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

        [HttpGet("teacher/{id}")]
        [Authorize(Roles = Roles.Teacher)]
        [ProducesResponseType(typeof(List<EarningViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetListByTeacherAccountId([FromRoute][Required] int id)
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

        [HttpGet("sum")]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetSumValue()
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

        [HttpGet("sum/teacher/{id}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetSumByTeacherAccountId([FromRoute][Required] int id)
        {
            try
            {
                var result = await _transactionService.GetSumByTeacherAccountId(id);
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

        [HttpGet("parent/{id}")]
        [Authorize(Roles = Roles.Parent)]
        [ProducesResponseType(typeof(List<TransactionViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByParentAccountId([FromRoute][Required] int id)
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

        [HttpPost("generateurl")]
        [Authorize(Roles = Roles.Parent)]
        [ProducesResponseType(typeof(TransactionRequest), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GenerateUrl(
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
                if (!parentObj)
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

        [HttpPost]
        [Authorize(Roles = $"{Roles.Admin}, {Roles.Parent}")]
        [ProducesResponseType(typeof(TransactionViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(
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
