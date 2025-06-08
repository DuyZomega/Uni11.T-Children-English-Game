using AutoMapper;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Admin.Create;
using CEG_BAL.ViewModels.Admin.Update;
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
    public class StudentHomeworkService : IStudentHomeworkService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public StudentHomeworkService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task Create(CreateNewStudentHomework newStuHom)
        {
            if (newStuHom == null)
                throw new ArgumentNullException(nameof(newStuHom), "The new student homework cannot be null.");

            // Validate required fields (optional)
            if ((await _unitOfWork.StudentProgressRepositories.GetByIdNoTracking( newStuHom.StudentProgressId)) == null)
                throw new ArgumentException("StudentProgressId not found.", nameof(newStuHom.StudentProgressId));

            // Validate required fields (optional)
            if ((await _unitOfWork.HomeworkRepositories.GetByIdNoTracking(newStuHom.HomeworkId)) == null)
                throw new ArgumentException("HomeworkId not found.", nameof(newStuHom.HomeworkId));

            // Validate required fields (optional)
            if ((await _unitOfWork.HomeworkResultRepositories.GetByIdNoTracking(newStuHom.HomeworkResultId)) == null)
                throw new ArgumentException("HomeworkResultId not found.", nameof(newStuHom.HomeworkResultId));

            var stuHom = new StudentHomework();
            _mapper.Map(newStuHom, stuHom);

            // Save to the database
            try
            {
                _unitOfWork.StudentHomeworkRepositories.Create(stuHom);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                // Log exception (if logging is configured)
                throw new Exception("An error occurred while creating the student homework.", ex);
            }
        }

        public async Task<StudentHomeworkViewModel?> GetById(int id)
        {
            var viewStuHom = await _unitOfWork.StudentHomeworkRepositories.GetByIdNoTracking(id);
            return viewStuHom != null ? _mapper.Map<StudentHomeworkViewModel>(viewStuHom) : null;
        }

        public async Task<List<StudentHomeworkViewModel>> GetList()
        {
            return _mapper.Map<List<StudentHomeworkViewModel>>(await _unitOfWork.StudentHomeworkRepositories.GetList());
        }

        public async Task<List<StudentHomeworkViewModel>?> GetListByAccountId(int id)
        {
            var studentId = await _unitOfWork.StudentRepositories.GetIdByAccountIdNoTracking(id);
            if (studentId == 0) return null;
            var list = await _unitOfWork.StudentHomeworkRepositories.GetListByStudentId(id);
            if (list.Count == 0) return null;
            var stuHomList = _mapper.Map<List<StudentHomeworkViewModel>>(list);
            for (int i = 0; i < stuHomList.Count; i++)
            {
                stuHomList[i].HomeworkNumber = i + 1;
            }
            return stuHomList;
        }

        public async Task Update(int stuHomId, UpdateStudentHomework upStuHom)
        {
            if (upStuHom == null)
                throw new ArgumentNullException(nameof(upStuHom), "New student homework cannot be null.");

            // Fetch the existing record
            var stuHom = await _unitOfWork.StudentHomeworkRepositories.GetByIdNoTracking(stuHomId)
                ?? throw new KeyNotFoundException("Student homework not found.");

            // Map changes from the update model to the entity
            _mapper.Map(upStuHom, stuHom);

            // Reattach entity and mark it as modified
            _unitOfWork.StudentHomeworkRepositories.Update(stuHom);

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
                throw new Exception("An unexpected error occurred while updating the student homework.", ex);
            }
        }
    }
}
