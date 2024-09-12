using CEG_BAL.Services.Implements;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Account;
using CEG_BAL.ViewModels.Account.Create;
using CEG_BAL.ViewModels.Authenticates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CEG_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITeacherService _teacherService;
        private readonly IParentService _parentService;
        private readonly IStudentService _studentService;
        private readonly IConfiguration _config;

        public AdminController(
            IAccountService accountService, ITeacherService teacherService, IParentService parentService, IStudentService studentService, IConfiguration config)
        {
            _accountService = accountService;
            _teacherService = teacherService;
            _parentService = parentService;
            _studentService = studentService;
            _config = config;
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(AccountService), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAccountById([FromRoute] int id)
        {
            try
            {
                var result = _accountService.GetById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "User Not Found!"
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
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [HttpPut("Update/{id}")]
        [ProducesResponseType(typeof(AccountViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(
            [FromRoute][Required] int id,
            [FromForm][Required] string username,
            [FromForm][Required] string role)
        {
            try
            {
                var result = await _accountService.GetById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Account does not exist !"
                    });
                }
                result.Username = username;
                result.Role.RoleName = role;
                _accountService.Update(result);
                result = await _accountService.GetById(id);
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
        [HttpPut("ChangePassword")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(AccountViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePassword(
            [FromBody][Required] UpdateAccountPassword upPass)
        {
            try
            {
                var result = await _accountService.GetById(Convert.ToInt32(upPass.AccountId));
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Account does not exist !"
                    });
                }
                if (!upPass.NewPassword.Equals(upPass.NewConfirmPassword))
                {
                    return BadRequest(new
                    {
                        Status = true,
                        ErrorMessage = "New Password and New Confirm Password are not the same !"
                    });
                }
                result.Password = upPass.NewPassword;
                _accountService.Update(result);
                return Ok(new
                {
                    Status = true,
                    Data = true
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

        [HttpPost("Account/Create")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(AccountViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAccount(
            [FromBody][Required] CreateNewAccount newAcc)
        {
            try
            {
                if (newAcc.Password == null || newAcc.Password == string.Empty)
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Password is Empty!"
                    });
                }
                var resultUsername = await _accountService.IsAccountExistByUsername(newAcc.Username);
                if (resultUsername)
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Username has already been taken!"
                    });
                }
                if (!newAcc.Password.Equals(newAcc.ConfirmPassword))
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Password and Confirm Password do not match!"
                    });
                }
                AccountViewModel value = new AccountViewModel()
                {
                    Username = newAcc.Username,
                    Password = newAcc.Password,
                };
                _accountService.Create(value, newAcc);
                return Ok(new
                {
                    Status = true,
                    SuccessMessage = "Account Create successfully !",
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

        [HttpPost("Teacher/Create")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(TeacherViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTeacher(
            [FromBody][Required] CreateNewTeacher newTeach)
        {
            try
            {
                if (newTeach.Account.Password == null || newTeach.Account.Password == string.Empty)
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Password is Empty!"
                    });
                }
                var resultEmail = await _teacherService.IsTeacherExistByEmail(newTeach.Email);
                if (resultEmail)
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Email has already been registered!"
                    });
                }
                var resultUsername = await _accountService.IsAccountExistByUsername(newTeach.Account.Username);
                if (resultUsername)
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Username has already been taken!"
                    });
                }
                if (!newTeach.Account.Password.Equals(newTeach.Account.ConfirmPassword))
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Password and Confirm Password do not match!"
                    });
                }
                TeacherViewModel teach = new TeacherViewModel()
                {
                    Email = newTeach.Email,
                    Phone = newTeach.Phone,
                    Address = newTeach.Address,
                };
                _teacherService.Create(teach, newTeach);
                return Ok(new
                {
                    Data = true,
                    Status = true,
                    SuccessMessage = "Teacher Account Create successfully !",
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

        [HttpPost("Parent/Create")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ParentViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateParent(
            [FromBody][Required] CreateNewParent newPar)
        {
            try
            {
                if (newPar.Account.Password == null || newPar.Account.Password == string.Empty)
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Password is Empty!"
                    });
                }
                var resultEmail = await _parentService.IsParentExistByEmail(newPar.Email);
                if (resultEmail)
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Email has already been registered!"
                    });
                }
                var resultUsername = await _accountService.IsAccountExistByUsername(newPar.Account.Username);
                if (resultUsername)
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Username has already been taken!"
                    });
                }
                if (!newPar.Account.Password.Equals(newPar.Account.ConfirmPassword))
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Password and Confirm Password do not match!"
                    });
                }
                ParentViewModel par = new ParentViewModel()
                {
                    Email = newPar.Email,
                    Phone = newPar.Phone,
                    Address = newPar.Address,
                };
                _parentService.Create(par, newPar);
                return Ok(new
                {
                    Data = true,
                    Status = true,
                    SuccessMessage = "Parent Account Create successfully !",
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

        [HttpPost("Student/Create")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(StudentViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateStudent(
            [FromBody][Required] CreateNewStudent newStu)
        {
            try
            {
                if (newStu.Account.Password == null || newStu.Account.Password == string.Empty)
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Password is Empty!"
                    });
                }
                var resultUsername = await _accountService.IsAccountExistByUsername(newStu.Account.Username);
                if (resultUsername)
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Username has already been taken!"
                    });
                }
                if (!newStu.Account.Password.Equals(newStu.Account.ConfirmPassword))
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Password and Confirm Password do not match!"
                    });
                }
                StudentViewModel stu = new StudentViewModel()
                {
                    Description = newStu.Description,
                    Highscore = newStu.Highscore,
                    Birhthdate = newStu.Birthdate,
                };
                _studentService.Create(stu, newStu);
                return Ok(new
                {
                    Data = true,
                    Status = true,
                    SuccessMessage = "Student Account Create successfully !",
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

        /*[HttpGet("GetId")]
        [Authorize(Roles = "Admin,Member")]
        [ProducesResponseType(typeof(AccountViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetIdByUsername(
            [FromQuery][Required] string username)
        {
            try
            {
                var result = await _accountService.GetIdByUsername(username);
                if (result == 0)
                {
                    throw new Exception("Member does not exist!");
                }
                return Ok(new
                {
                    Status = true,
                    result
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
        /*[HttpPost("CreateTeacher")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(TeacherViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTeacher(
            [FromBody][Required] CreateNewTeacher newAcc)
        {
            try
            {
                if (newAcc.Account.Password == null || newAcc.Account.Password == string.Empty)
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Password is Empty !"
                    });
                }
                //var result = await _accountService.GetByEmailModel(newAcc.Email);
                //if (result != null)
                //{
                //    return BadRequest(new
                //    {
                //        Status = false,
                //        ErrorMessage = "Email has already registered !"
                //    });
                //}
                if (!newAcc.Account.Password.Equals(newAcc.Account.ConfirmPassword))
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Password and Confirm Password are not the same !"
                    });
                }
                AccountViewModel value = new AccountViewModel()
                {
                    Username = newAcc.Account.Username,
                    Password = newAcc.Account.Password
                };
                _teacherService.Create(value, newAcc);
                var loguser = new AuthenRequest()
                {
                    Username = newAcc.Account.Username,
                    Password = newAcc.Account.Password
                };
                var resultaft = await _accountService.AuthenticateAccount(loguser);

                if (resultaft == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new
                    {
                        Status = false,
                        ErrorMessage = "Error while Registering your Account !"
                    });
                }
                return Ok(new
                {
                    Status = true,
                    SuccessMessage = "Teacher Account Create successfully !",
                    Data = resultaft
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
    }
}
