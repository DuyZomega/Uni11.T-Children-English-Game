using AutoMapper;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Account.Create;
using CEG_BAL.ViewModels.Admin.Update;
using CEG_BAL.ViewModels.Home;
using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace CEG_BAL.Services.Implements
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJWTService _jwtService;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public StudentService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IJWTService jwtServices,
            IConfiguration configuration,
            IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtService = jwtServices;
            _configuration = configuration;
            _emailService = emailService;
        }
        public void Create(StudentViewModel student, CreateNewStudent newStu)
        {
            var acc = _mapper.Map<Student>(student);
            acc.Account.CreatedDate = DateTime.Now;
            acc.Account.Status = "Active";
            acc.Account.RoleId = _unitOfWork.RoleRepositories.GetRoleIdByRoleName("Student").Result;
            if (_unitOfWork.ParentRepositories.GetIdByFullname(newStu.ParentFullname).Result == 0) throw new Exception("Parent Not Found!");
            if (newStu != null)
            {
                acc.Account.Fullname = newStu.Account.Fullname;
                acc.Account.Username = newStu.Account.Username;
                acc.Account.Gender = newStu.Account.Gender;
                acc.Account.Password = newStu.Account.Password;
                acc.Account.RoleId = 2;
                acc.Description = newStu.Description;
                //acc.TotalPoint = newStu.TotalPoints;
                acc.Birthdate = newStu.Birthdate;
                acc.Age = CalculateAge(acc.Birthdate.Value);
                acc.ParentId = _unitOfWork.ParentRepositories.GetIdByFullname(newStu.ParentFullname).Result;
            }
            _unitOfWork.StudentRepositories.Create(acc);
            _unitOfWork.Save();
            var parent = _unitOfWork.ParentRepositories.GetByIdNoTracking(acc.ParentId).Result;
            _ = _emailService.SendEmailAsync(
                    _configuration.GetSection("Gmail:SenderName").Value,
                    _configuration.GetSection("Gmail:Username").Value,
                    newStu.ParentFullname,
                    parent.Email,
                    "Thank you for chosing us!",
                    "   <h2>Your Children's Student Account has been created successfully!</h2>" +
                    "<div>" +
                    "   <h3>These below are your child student account username and password:</h3>" +
                    "   <h4>Username: " + newStu.Account.Username + "</h4>" +
                    "   <h4>Password: " + newStu.Account.Password + "</h4>" +
                    "</div>",
                    _configuration,
                    _configuration.GetSection("Gmail:Username").Value,
                    _configuration.GetSection("Gmail:Password").Value
                ).Result;
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
            var user = await _unitOfWork.StudentRepositories.GetByIdNoTracking(id);
            return user != null ? _mapper.Map<StudentViewModel>(user) : null;
        }

        public async Task<List<StudentViewModel>> GetStudentList()
        {
            return _mapper.Map<List<StudentViewModel>>(await _unitOfWork.StudentRepositories.GetStudentList());
        }

        public async Task<List<string>> GetStudentNameList()
        {
            return await _unitOfWork.StudentRepositories.GetStudentNameList();
        }

        public async Task<List<string>> GetStudentNameListByParent(int id)
        {
            var parentId = await _unitOfWork.ParentRepositories.GetIdByAccountIdNoTracking(id);
            if (parentId == 0) return null;
            return await _unitOfWork.StudentRepositories.GetStudentNameListByParent(parentId);
        }

        public async Task<List<string>?> GetFullnameListByParentName(string parentName)
        {
            return await _unitOfWork.StudentRepositories.GetFullnameListByParentName(parentName);
        }

        public async Task<List<StudentViewModel>> GetStudentByParentAccountId(int id)
        {
            var parentId = await _unitOfWork.ParentRepositories.GetIdByAccountIdNoTracking(id);
            if (parentId == 0) return null;
            return _mapper.Map<List<StudentViewModel>>(await _unitOfWork.StudentRepositories.GetStudentByParentId(parentId));
        }
        public async Task<List<StudentViewModel>> GetStudentByClassId(int id)
        {
            var classId = await _unitOfWork.ClassRepositories.GetIdByClassId(id);
            if (classId == 0) return null;
            return _mapper.Map<List<StudentViewModel>>(await _unitOfWork.StudentRepositories.GetStudentByClassId(classId));
        }
        public async Task<List<StudentLeaderboard>> GetStudentListByPointRank()
        {
            // Fetch anonymous objects from repository
            var studentsWithPoints = await _unitOfWork.StudentRepositories.GetStudentListWithTotalPoints();

            // Map to DTOs
            var rankedStudents = studentsWithPoints
                .Select((student, index) => new StudentLeaderboard
                {
                    StudentName = (string)student.GetType().GetProperty("StudentName")?.GetValue(student),
                    Rank = index + 1,
                    Points = (int)student.GetType().GetProperty("TotalPoints")?.GetValue(student)
                })
                .ToList();

            return rankedStudents;
        }
        public void Update(StudentViewModel student, UpdateStudent studentNewInfo)
        {
            var stu = _mapper.Map<Student>(student);
            if(studentNewInfo != null)
            {
                stu.StudentId = _unitOfWork.StudentRepositories.GetIdByAccountIdNoTracking(stu.Account.AccountId).Result.Value;
                stu.AccountId = stu.Account.AccountId;
                stu.Account.Fullname = studentNewInfo.Account.Fullname;
                stu.Account.Gender = studentNewInfo.Account.Gender;
                stu.Birthdate = studentNewInfo.Birthdate;
                stu.Age = CalculateAge(stu.Birthdate.Value);
                stu.Description = studentNewInfo.Description;
                stu.ParentId = _unitOfWork.ParentRepositories.GetIdByFullname(studentNewInfo.ParentFullname).Result;
            }
            stu.Parent = null;
            stu.Enrolls = null;
            stu.StudentProgresses = null;
            _unitOfWork.StudentRepositories.Update(stu); 
            _unitOfWork.Save();
        }
        private int CalculateAge(DateTime birthdate)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - birthdate.Year;

            // Check if the birthday has not occurred yet this year
            if (birthdate.Date > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }

        public async Task<int> GetTotalAmountByParent(int id)
        {
            var parentId = await _unitOfWork.ParentRepositories.GetIdByAccountIdNoTracking(id);
            if (parentId == 0) throw new Exception("Parent not found.");
            return await _unitOfWork.StudentRepositories.GetTotalAmountByParent(parentId);
        }

        public async Task<int> GetTotalAmountByTeacher(int id)
        {
            var teacherId = await _unitOfWork.TeacherRepositories.GetIdByAccountId(id);
            if (teacherId == 0) throw new Exception("Teacher not found.");
            return await _unitOfWork.StudentRepositories.GetStudentCountByTeacherId(teacherId);
        }
    }
}
