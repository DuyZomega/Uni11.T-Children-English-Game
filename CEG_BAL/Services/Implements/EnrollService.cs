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
        public void Create(EnrollViewModel model, CreateNewEnroll newEn)
        {
            var c = _unitOfWork.ClassRepositories.GetByClassName(newEn.ClassName).Result;
            var s = _unitOfWork.StudentRepositories.GetByFullname(newEn.StudentName).Result;
            var t = _unitOfWork.TransactionRepositories.GetById(newEn.TransactionId);

            var en = _mapper.Map<Enroll>(model);
            if (newEn != null)
            {
                en.ClassId = c.ClassId;
                en.StudentId = s.StudentId;
                en.TransactionId = t.TransactionId;
                en.RegistrationDate = DateTime.Now;
                en.EnrolledDate = DateTime.Now;
                en.Status = "Enrolled";
                //en.Class = c;
                //en.Student = s;
                //en.Transaction = t;
            }
            _unitOfWork.EnrollRepositories.Create(en);
            _unitOfWork.Save();
        }

        public async Task<EnrollViewModel> GetEnrollById(int id)
        {
            var user = await _unitOfWork.EnrollRepositories.GetByIdNoTracking(id);
            if (user != null)
            {
                var urs = _mapper.Map<EnrollViewModel>(user);
                return urs;
            }
            return null;
        }

        public async Task<List<EnrollViewModel>> GetEnrollsList()
        {
            return _mapper.Map<List<EnrollViewModel>>(await _unitOfWork.EnrollRepositories.GetEnrollsList());
        }

        public async Task<List<EnrollViewModel>> GetEnrollByParentAccountId(int id)
        {
            var parentId = await _unitOfWork.ParentRepositories.GetIdByAccountId(id);
            if (parentId == 0) return null;
            return _mapper.Map<List<EnrollViewModel>>(await _unitOfWork.EnrollRepositories.GetEnrollByParentId(parentId));
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
    }
}
