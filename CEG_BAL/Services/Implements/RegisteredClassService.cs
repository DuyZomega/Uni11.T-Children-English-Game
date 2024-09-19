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
    public class RegisteredClassService : IRegisteredClassService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJWTService _jwtService;
        private readonly IConfiguration _configuration;

        public RegisteredClassService(
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
        public void Create(RegisteredClassViewModel model)
        {
            var regi = _mapper.Map<RegisteredClass>(model);
            _unitOfWork.RegisteredCourseRepositories.Create(regi);
            _unitOfWork.Save();
        }

        public async Task<List<RegisteredClassViewModel>> GetAllRegisteredCourse()
        {
            return _mapper.Map<List<RegisteredClassViewModel>>(await  _unitOfWork.RegisteredCourseRepositories.GetRegisteredCoursesList());
        }

        public async Task<RegisteredClassViewModel> GetRegisteredCourseById(int id)
        {
            var user = await _unitOfWork.RegisteredCourseRepositories.GetByIdNoTracking(id);
            if (user != null) {
                var urs = _mapper.Map<RegisteredClassViewModel>(user);
                return urs;
            }
            return null;
        }

        public void Update(RegisteredClassViewModel model)
        {
            var regi = _mapper.Map<RegisteredClass>(model);
            _unitOfWork.RegisteredCourseRepositories.Update(regi);
            _unitOfWork.Save();
        }
    }
}
