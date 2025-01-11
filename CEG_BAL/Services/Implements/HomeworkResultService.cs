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
    public class HomeworkResultService : IHomeworkResultService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJWTService _jwtService;
        private readonly IConfiguration _configuration;

        public HomeworkResultService(
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
        public async Task Create(CreateNewHomeworkResult newHomRes)
        {
            if (newHomRes == null)
                throw new ArgumentNullException(nameof(newHomRes), "The new homework result cannot be null.");

            var stuHom = new HomeworkResult();
            _mapper.Map(newHomRes, stuHom);

            // Save to the database
            try
            {
                _unitOfWork.HomeworkResultRepositories.Create(stuHom);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                // Log exception (if logging is configured)
                throw new Exception("An error occurred while creating the homework result.", ex);
            }
        }

        // GetByStudentIdAndHomeworkIdNoTracking

        public async Task<List<HomeworkResultViewModel>> GetList()
        {
            return _mapper.Map<List<HomeworkResultViewModel>>(await _unitOfWork.HomeworkResultRepositories.GetList());
        }

        public async Task<HomeworkResultViewModel?> GetById(int id)
        {
            var viewHomRes = await _unitOfWork.HomeworkResultRepositories.GetByIdNoTracking(id);
            return viewHomRes != null ? _mapper.Map<HomeworkResultViewModel>(viewHomRes) : null;
        }

        public async Task<HomeworkResultViewModel?> GetByStudentIdAndHomeworkId(int stuId, int homId)
        {
            var viewHomRes = await _unitOfWork.HomeworkResultRepositories.GetByStudentIdAndHomeworkIdNoTracking(stuId, homId);
            return viewHomRes != null ? _mapper.Map<HomeworkResultViewModel>(viewHomRes) : null;
        }

        public async Task Update(int homeResId, UpdateHomeworkResult upHomRes)
        {
            if (upHomRes == null)
                throw new ArgumentNullException(nameof(upHomRes), "New homework result cannot be null.");

            // Fetch the existing record
            var homRes = await _unitOfWork.HomeworkResultRepositories.GetByIdNoTracking(homeResId)
                ?? throw new KeyNotFoundException("Homework result not found.");

            // Map changes from the update model to the entity
            _mapper.Map(upHomRes, homRes);

            // Reattach entity and mark it as modified
            _unitOfWork.HomeworkResultRepositories.Update(homRes);

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
                throw new Exception("An unexpected error occurred while updating the homework result.", ex);
            }
        }
    }
}
