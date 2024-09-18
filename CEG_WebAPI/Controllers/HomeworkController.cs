using CEG_BAL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CEG_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeworkController : ControllerBase
    {
        private readonly IHomeworkService _homeworkService;
        private readonly IConfiguration _config;

        public HomeworkController(IHomeworkService homeworkService, IConfiguration config)
        {
            _homeworkService = homeworkService;
            _config = config;
        }
    }
}
