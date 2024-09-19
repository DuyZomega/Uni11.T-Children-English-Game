using CEG_BAL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CEG_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        private readonly IConfiguration _config;

        public TeacherController(
            ITeacherService teacherService, IConfiguration config)
        {
            _teacherService = teacherService;
            _config = config;
        }
    }
}
