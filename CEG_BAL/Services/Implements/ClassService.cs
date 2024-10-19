using AutoMapper;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Admin;
using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CEG_BAL.Configurations.Constants;

namespace CEG_BAL.Services.Implements
{
    public class ClassService : IClassService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJWTService _jwtService;
        private readonly IConfiguration _configuration;

        public ClassService(
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

        public void Create(ClassViewModel classModel, CreateNewClass newClass)
        {
            var clas = _mapper.Map<Class>(classModel);
            if (newClass != null)
            {
                clas.ClassName = newClass.ClassName;
                clas.TeacherId = _unitOfWork.TeacherRepositories.GetByFullname(newClass.TeacherName).Result.TeacherId;
                clas.CourseId = _unitOfWork.CourseRepositories.GetIdByName(newClass.CourseName).Result;
                clas.StartDate = newClass.StartDate;
                clas.EndDate = newClass.EndDate;
                clas.MinimumStudents = newClass.MinStudents;
                clas.MaximumStudents = newClass.MaxStudents;
            }
            _unitOfWork.ClassRepositories.Create(clas);
            _unitOfWork.Save();
        }

        public async Task<ClassViewModel?> GetClassById(int id)
        {
            var user = await _unitOfWork.ClassRepositories.GetByIdNoTracking(id);
            if(user != null)
            {
                var usr = _mapper.Map<ClassViewModel>(user);
                return usr;
            }
            return null;
        }

        public async Task<List<ClassViewModel>> GetClassList()
        {
            return _mapper.Map<List<ClassViewModel>>(await _unitOfWork.ClassRepositories.GetClassList());
        }

        public async Task<List<ClassViewModel>> GetClassListAdmin()
        {
            return _mapper.Map<List<ClassViewModel>>(await _unitOfWork.ClassRepositories.GetClassListAdmin());
        }
        public async Task<List<ClassViewModel>> GetClassListByTeacherAccountId(int id)
        {
            var teacherId = await _unitOfWork.TeacherRepositories.GetIdByAccountId(id);
            if (teacherId == 0) return null;
            return _mapper.Map<List<ClassViewModel>>(await _unitOfWork.ClassRepositories.GetClassListByTeacherId(teacherId));
        }
        public void Update(ClassViewModel classModel)
        {
            var clas = _mapper.Map<Class>(classModel);
            _unitOfWork.ClassRepositories.Update(clas);
            _unitOfWork.Save();
        }

        public void UpdateStatus(int classId, string classStatus)
        {
            var clas = _unitOfWork.ClassRepositories.GetByIdNoTracking(classId).Result;
            if (clas == null) return;
            clas.Status = classStatus;
            _unitOfWork.ClassRepositories.Update(clas);
            _unitOfWork.Save();
        }
    }
}
