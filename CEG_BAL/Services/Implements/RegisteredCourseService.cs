using AutoMapper;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Implements
{
    public class RegisteredCourseService : IRegisteredCourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJWTService _jwtService;
        private readonly IConfiguration _configuration;

        public RegisteredCourseService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IJWTService jwtServices,
            IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtService = jwtServices;
            _configuration = configuration;
        }
        public void Create(RegisteredCourseViewModel model)
        {
            var regi = _mapper.Map<RegisteredCourse>(model);
            _unitOfWork.RegisteredCourseRepositories.Create(regi);
            _unitOfWork.Save();
        }

        public async Task<List<RegisteredCourseViewModel>> GetAllRegisteredCourse()
        {
            return _mapper.Map<List<RegisteredCourseViewModel>>(await  _unitOfWork.RegisteredCourseRepositories.GetRegisteredCoursesList());
        }

        public async Task<RegisteredCourseViewModel> GetRegisteredCourseById(int id)
        {
            var user = await _unitOfWork.RegisteredCourseRepositories.GetByIdNoTracking(id);
            if (user != null) {
                var urs = _mapper.Map<RegisteredCourseViewModel>(user);
                return urs;
            }
            return null;
        }

        public void Update(RegisteredCourseViewModel model)
        {
            var regi = _mapper.Map<RegisteredCourse>(model);
            _unitOfWork.RegisteredCourseRepositories.Update(regi);
            _unitOfWork.Save();
        }
    }
}
