using AutoMapper;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
using CEG_DAL.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CEG_DAL.Models;

namespace CEG_BAL.Services.Implements
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJWTService _jwtService;
        private readonly IConfiguration _configuration;

        public CourseService(
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
        public void Create(CourseViewModel course)
        {
            var cou = _mapper.Map<Course>(course);
            _unitOfWork.CourseRepositories.Create(cou);
            _unitOfWork.Save();
        }

        public async Task<CourseViewModel> GetCourseById(int id)
        {
            var user = await _unitOfWork.CourseRepositories.GetByIdNoTracking(id);
            if (user != null)
            {
                var urs = _mapper.Map<CourseViewModel>(user);
                return urs;
            }
            return null;
        }

        public async Task<List<CourseViewModel>> GetCourseList()
        {
            return _mapper.Map<List<CourseViewModel>>(await _unitOfWork.CourseRepositories.GetCoursList());
        }

        public void Update(CourseViewModel course)
        {
            var cou = _mapper.Map<Course>(course);
            _unitOfWork.CourseRepositories.Update(cou);
            _unitOfWork.Save();
        }
    }
}
