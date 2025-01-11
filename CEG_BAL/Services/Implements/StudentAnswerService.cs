using AutoMapper;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Admin.Create;
using CEG_BAL.ViewModels.Admin.Update;
using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CEG_BAL.Services.Implements
{
    public class StudentAnswerService : IStudentAnswerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public StudentAnswerService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task Create(CreateNewStudentAnswer newStuAns)
        {
            if (newStuAns == null)
                throw new ArgumentNullException(nameof(newStuAns), "The new homework result cannot be null.");

            // Validate required fields (optional)
            if ((await _unitOfWork.StudentHomeworkRepositories.GetByIdNoTracking(newStuAns.StudentHomeworkId)) == null)
                throw new ArgumentException("StudentHomeworkId not found.", nameof(newStuAns.StudentHomeworkId));

            var stuAns = new StudentAnswer();
            _mapper.Map(newStuAns, stuAns);

            // Save to the database
            try
            {
                _unitOfWork.StudentAnswerRepositories.Create(stuAns);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                // Log exception (if logging is configured)
                throw new Exception("An error occurred while creating the homework result.", ex);
            }
        }

        public async Task<StudentAnswerViewModel?> GetById(int id)
        {
            var answ = await _unitOfWork.StudentAnswerRepositories.GetByIdNoTracking(id);
            if (answ != null)
            {
                var answvm = _mapper.Map<StudentAnswerViewModel>(answ);
                return answvm;
            }
            return null;
        }

        public async Task<List<StudentAnswerViewModel>> GetList()
        {
            return _mapper.Map<List<StudentAnswerViewModel>>(await _unitOfWork.StudentAnswerRepositories.GetList());
        }

        public async Task Update(int stuAnsId, UpdateStudentAnswer upStuAns)
        {
            if (upStuAns == null)
                throw new ArgumentNullException(nameof(upStuAns), "New student answer cannot be null.");

            // Fetch the existing record
            var stuAns = await _unitOfWork.StudentAnswerRepositories.GetByIdNoTracking(stuAnsId)
                ?? throw new KeyNotFoundException("Student answer not found.");

            // Map changes from the update model to the entity
            _mapper.Map(upStuAns, stuAns);

            // Reattach entity and mark it as modified
            _unitOfWork.StudentAnswerRepositories.Update(stuAns);

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
                throw new Exception("An unexpected error occurred while updating the student answer.", ex);
            }
        }
    }
}
