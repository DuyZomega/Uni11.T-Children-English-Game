using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Account.Create;
using CEG_BAL.ViewModels.Authenticates;
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
                        ErrorMessage = "User Account is Suspended! Due to your violations of our club guidelines",
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
                        ErrorMessage = "Sorry, your registration request has been denied by the Birdclub manager",
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
        [HttpPost("Register")]
        [ProducesResponseType(typeof(AccountViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser(
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
                _accountService.CreateAccount(value, newAcc);
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
                        ErrorMessage = "Error while Registering your Account !"

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
                if (ex.InnerException != null)
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = ex.Message,
                        InnerExceptionMessage = ex.InnerException.Message
                    });
                }
                // Log the exception if needed
                return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = ex.Message
                });
            }
        }
    }
}
