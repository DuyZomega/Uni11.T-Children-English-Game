using AutoMapper;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Parent;
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
    public class EnrollService : IEnrollService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJWTService _jwtService;
        private readonly IConfiguration _configuration;

        public EnrollService(
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
        public async Task Create(CreateNewEnroll newEn)
        {
            if (newEn == null)
                throw new ArgumentNullException(nameof(newEn), "The new enrollment info cannot be null.");

            var cla = 
                await _unitOfWork.ClassRepositories.GetByClassName(newEn.ClassName) ?? 
                throw new ArgumentNullException(newEn.ClassName, "Class info not found.");
            var stu = await _unitOfWork.StudentRepositories.GetByFullname(newEn.StudentName) ??
                throw new ArgumentNullException(newEn.StudentName, "Student info not found.");
            var tra = await _unitOfWork.TransactionRepositories.GetByIdNoTracking(newEn.TransactionId) ??
                throw new ArgumentNullException(newEn.TransactionId.ToString(), "Transaction info not found.");

            var enr = new Enroll();
            _mapper.Map(newEn, enr);
            enr.ClassId = cla.ClassId;
            enr.StudentId = stu.StudentId;

            // Save to the database
            try
            {
                _unitOfWork.EnrollRepositories.Create(enr);

                // Update the NumberOfStudents field for the class
                cla.NumberOfStudents++;
                _unitOfWork.ClassRepositories.Update(cla);
                
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                // Log exception (if logging is configured)
                throw new Exception("An error occurred while creating enrollment.", ex);
            }
        }

        public async Task<EnrollViewModel?> GetById(int id)
        {
            var user = await _unitOfWork.EnrollRepositories.GetByIdNoTracking(id);
            return user == null ? null : _mapper.Map<EnrollViewModel>(user);
        }

        public async Task<List<EnrollViewModel>> GetList()
        {
            return _mapper.Map<List<EnrollViewModel>>(await _unitOfWork.EnrollRepositories.GetList());
        }

        public async Task<List<EnrollViewModel>?> GetEnrollByParentAccountId(int id)
        {
            var parentId = await _unitOfWork.ParentRepositories.GetIdByAccountIdNoTracking(id);
            return parentId == 0 ? null : _mapper.Map<List<EnrollViewModel>>(await _unitOfWork.EnrollRepositories.GetEnrollByParentId(parentId));
        }

        public async Task<List<EnrollViewModel>?> GetEnrollByStudentAccountId(int id)
        {
            var studentId = await _unitOfWork.StudentRepositories.GetIdByAccountIdNoTracking(id);
            return studentId == 0 ? null : _mapper.Map<List<EnrollViewModel>>(await _unitOfWork.EnrollRepositories.GetEnrollByStudentId(studentId));
        }

        public void Update(EnrollViewModel model)
        {
            var en = _mapper.Map<Enroll>(model);
            _unitOfWork.EnrollRepositories.Update(en);
            _unitOfWork.Save();
        }
        public void UpdateStatus(int enrollId, string enrollStatus)
        {
            var enr = _unitOfWork.EnrollRepositories.GetByIdNoTracking(enrollId).Result;
            if (enr == null) return;
            enr.Status = enrollStatus;
            _unitOfWork.EnrollRepositories.Update(enr);
            _unitOfWork.Save();
        }

        public async Task<EnrollViewModel?> GetByStudentFullnameAndClassName(string stuFullname, string claName)
        {
            var existEnr = await _unitOfWork.EnrollRepositories.GetByStudentFullnameAndClassName(stuFullname,claName);
            return existEnr != null ? _mapper.Map<EnrollViewModel>(existEnr) : null;
        }
    }
}
