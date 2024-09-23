using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Account.Create;
using CEG_BAL.ViewModels.Authenticates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CEG_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(
            IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("Login")]
        [ProducesResponseType(typeof(AuthenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAccountLogin(
            [FromBody] AuthenRequest logaccount)
        {
            try
            {
                var result = await _accountService.AuthenticateAccount(logaccount);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Username or password is invalid!"
                    });
                }
                if (result.Status == "Suspended")
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "User Account is Suspended!",
                        Data = result
                    });
                }
                if (result.Status == "Inactive")
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "User Account is Currently InActivated!",
                        Data = result
                    });
                }
                if (result.Status == "Expired")
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "User Account is Currently Expired! Please renew your Membership",
                        Data = result
                    });
                }
                if (result.Status == "Denied")
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Sorry, your registration request has been denied by the CEG manager",
                        Data = result
                    });
                }
                return Ok(new
                {
                    Status = true,
                    SuccessMessage = "Login successfully!",
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

        [HttpGet("All")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(List<AccountViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAccountList()
        {
            try
            {
                var result = await _accountService.GetAccountList();
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Account List Not Found!"
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
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(AccountViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAccountById([FromRoute] int id)
        {
            try
            {
                var result = await _accountService.GetById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Account Not Found!"
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

        [HttpPut("{id}/Update")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(AccountViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromRoute] int id, AccountViewModel account)
        {
            try
            {
                var result = await _accountService.GetById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Account Does Not Exist"
                    });
                }
                account.AccountId = id;
                _accountService.Update(account);
                result = await _accountService.GetById(account.AccountId.Value);
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

        [HttpPost("{id}/Disable")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(AccountViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Disable([FromRoute] int id)
        {
            try
            {
                var result = await _accountService.GetById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "Account Does Not Exist"
                    });
                }
                result.AccountId = id;
                result.Status = "Inactive";
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

        [HttpPost("Register")]
        [ProducesResponseType(typeof(AccountViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
                        ErrorMessage = "Password is Empty !"
                    });
                }
                //var result = await _userService.GetByEmailModel(newAcc.Email);
                //if (result != null)
                //{
                //    return BadRequest(new
                //    {
                //        Status = false,
                //        ErrorMessage = "Email has already registered !"
                //    });
                //}
                if (!newAcc.Password.Equals(newAcc.ConfirmPassword))
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = "Password and Confirm Password are not the same !"
                    });
                }
                AccountViewModel value = new AccountViewModel()
                {
                    Username = newAcc.Username,
                    Password = newAcc.Password
                };
                _accountService.Create(value, newAcc);
                var loguser = new AuthenRequest()
                {
                    Username = newAcc.Username,
                    Password = newAcc.Password
                };
                var resultaft = await _accountService.AuthenticateAccount(loguser);

                if (resultaft == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new
                    {
                        Status = false,
                        ErrorMessage = "Error while Creating new Account !"
                    });
                }
                return Ok(new
                {
                    Status = true,
                    SuccessMessage = "Account Create successfully !",
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
        }
    }
}
