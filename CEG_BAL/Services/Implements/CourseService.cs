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
using CEG_BAL.ViewModels.Admin;

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
        public void Create(CourseViewModel course, CreateNewCourse newCourse)
        {
            var cou = _mapper.Map<Course>(course);
            cou.Status = "Draft";
            cou.Image = "Image";
            if(newCourse != null)
            {
                cou.CourseName = newCourse.CourseName;
                cou.CourseType = newCourse.CourseType;
                cou.Description = newCourse.Description;
                cou.Image = newCourse.Image;
                cou.TotalHours = newCourse.TotalHours;
                cou.RequiredAge = newCourse.RequiredAge;
                cou.Difficulty = newCourse.Difficulty;
                cou.Category = newCourse.Category;
            }
            _unitOfWork.CourseRepositories.Create(cou);
            _unitOfWork.Save();
        }

        public async Task<CourseViewModel?> GetCourseById(int id)
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
            return _mapper.Map<List<CourseViewModel>>(await _unitOfWork.CourseRepositories.GetCourseList());
        }

        public void Update(CourseViewModel course)
        {
            var cou = _mapper.Map<Course>(course);
            _unitOfWork.CourseRepositories.Update(cou);
            _unitOfWork.Save();
        }

        public async Task<bool> IsCourseExistByName(string name)
        {
            var cou = await _unitOfWork.CourseRepositories.GetByName(name);
            if (cou != null) return true;
            return false;
        }
    }
}
