using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels.Authenticates;
using Microsoft.AspNetCore.Mvc;

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
    }
}
