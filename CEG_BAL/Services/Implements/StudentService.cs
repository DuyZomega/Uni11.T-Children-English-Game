using AutoMapper;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Account.Create;
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
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJWTService _jwtService;
        private readonly IConfiguration _configuration;

        public StudentService(
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
        public void Create(StudentViewModel student, CreateNewStudent newStu)
        {
            var acc = _mapper.Map<Student>(student);
            acc.Account.CreatedDate = DateTime.Now;
            acc.Account.Status = "Active";
            acc.Account.RoleId = _unitOfWork.RoleRepositories.GetRoleIdByRoleName("Student").Result;
            if (newStu != null)
            {
                acc.Account.Fullname = newStu.Account.Fullname;
                acc.Account.Username = newStu.Account.Username;
                acc.Account.Gender = newStu.Account.Gender;
                acc.Account.Password = newStu.Account.Password;
                acc.Description = newStu.Description;
                acc.TotalPoint = newStu.TotalPoints;
                acc.Birthdate = newStu.Birthdate;
                acc.ParentId = _unitOfWork.ParentRepositories.GetIdByUsername(newStu.ParentUsername).Result;
            }
            _unitOfWork.StudentRepositories.Create(acc);
            _unitOfWork.Save();
        }

        public async Task<StudentViewModel?> GetStudentByAccountId(int id)
        {
            var user = await _unitOfWork.StudentRepositories.GetByAccountIdNoTracking(id);
            if (user != null)
            {
                var usr = _mapper.Map<StudentViewModel>(user);
                return usr;
            }
            return null;
        }

        public async Task<StudentViewModel?> GetStudentById(int id)
        {
            var user = await _unitOfWork.AccountRepositories.GetByIdNoTracking(id);
            if (user != null)
            {
                //var mem = await _unitOfWork.MemberRepository.GetByIdNoTracking(user.MemberId);
                var usr = _mapper.Map<StudentViewModel>(user);
                return usr;
            }
            return null;
        }

        public async Task<List<StudentViewModel>> GetStudentList()
        {
            return _mapper.Map<List<StudentViewModel>>(await _unitOfWork.StudentRepositories.GetStudentList());
        }

        public async Task<List<StudentViewModel>> GetStudentByParentAccountId(int id)
        {
            var parentId = await _unitOfWork.ParentRepositories.GetIdByAccountId(id);
            if (parentId == 0) return null;
            return _mapper.Map<List<StudentViewModel>>(await _unitOfWork.StudentRepositories.GetStudentByParentId(parentId));
        }

        public void Update(StudentViewModel student)
        {
            var stu = _mapper.Map<Student>(student);
             _unitOfWork.StudentRepositories.Update(stu); 
            _unitOfWork.Save();
        }
    }
}
