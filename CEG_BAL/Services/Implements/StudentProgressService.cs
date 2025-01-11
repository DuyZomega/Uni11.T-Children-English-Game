using AutoMapper;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Admin.Create;
using CEG_BAL.ViewModels.Admin.Update;
using CEG_BAL.ViewModels.Student;
using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Implements
{
    public class StudentProgressService : IStudentProgressService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StudentProgressService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Create(CreateNewStudentProgress newStuPro)
        {
            if (newStuPro == null)
                throw new ArgumentNullException(nameof(newStuPro), "The new student progress cannot be null.");

            var stuPro = new StudentProgress();
            _mapper.Map(newStuPro, stuPro);

            // Save to the database
            try
            {
                _unitOfWork.StudentProgressRepositories.Create(stuPro);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                // Log exception (if logging is configured)
                throw new Exception("An error occurred while creating the student progress.", ex);
            }
        }

        public async Task<StudentProgressViewModel?> GetById(int id)
        {
            var viewStuPro = await _unitOfWork.StudentProgressRepositories.GetByIdNoTracking(id);
            return viewStuPro != null ? _mapper.Map<StudentProgressViewModel>(viewStuPro) : null;
        }

        public async Task<List<StudentProgressViewModel>> GetList()
        {
            return _mapper.Map<List<StudentProgressViewModel>>(await _unitOfWork.StudentProgressRepositories.GetList());
        }

        public async Task Update(int stuProId, UpdateStudentProgress upStuPro)
        {
            if (upStuPro == null)
                throw new ArgumentNullException(nameof(upStuPro), "New student progress cannot be null.");

            // Fetch the existing record
            var stuPro = await _unitOfWork.StudentProgressRepositories.GetByIdNoTracking(stuProId)
                ?? throw new KeyNotFoundException("Student progress not found.");

            // Map changes from the update model to the entity
            _mapper.Map(upStuPro, stuPro);

            // Reattach entity and mark it as modified
            _unitOfWork.StudentProgressRepositories.Update(stuPro);

            // Save changes
            try
            {
                _unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Handle concurrency issues (e.g., row modified by another user)
                throw new InvalidOperationException("Update failed due to a concurrency conflict.", ex);
            }
            catch (Exception ex)
            {
                // Log and rethrow unexpected exceptions
                throw new Exception("An unexpected error occurred while updating the student progress.", ex);
            }
        }

        public async Task<StudentDashboard> GetByStudentAccountId(int id)
        {
            var studentId = await _unitOfWork.StudentRepositories.GetIdByAccountIdNoTracking(id);
            if (studentId == null) return null;
            var stuDash = new StudentDashboard
            {
                TotalPlaytime = await _unitOfWork.StudentProgressRepositories.GetTotalTimeByStudentId(studentId),
                TotalPoints = await _unitOfWork.StudentProgressRepositories.GetTotalPointByStudentId(studentId),
            };
            return stuDash;
        }
    }
}
