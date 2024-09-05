using Microsoft.AspNetCore.Mvc;

namespace CEG_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
