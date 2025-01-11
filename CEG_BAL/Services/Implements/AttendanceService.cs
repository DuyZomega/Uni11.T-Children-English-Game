using AutoMapper;
using CEG_BAL.Configurations;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Admin;
using CEG_BAL.ViewModels.Admin.Update;
using CEG_DAL.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Implements
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJWTService _jwtService;
        private readonly IConfiguration _configuration;

        public AttendanceService(
            IUnitOfWork unitOfWork, 
            IMapper mapper, 
            IJWTService jwtService, 
            IConfiguration configuration
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtService = jwtService;
            _configuration = configuration;
        }

        public Task Create(CreateNewClass newCla)
        {
            throw new NotImplementedException();
        }

        public async Task<AttendanceViewModel?> GetById(int id)
        {
            return _mapper.Map<AttendanceViewModel>(await _unitOfWork.AttendanceRepositories.GetByIdNoTracking(id));
        }

        public async Task<List<AttendanceViewModel>> GetListNoTracking()
        {
            return _mapper.Map<List<AttendanceViewModel>>(await _unitOfWork.AttendanceRepositories.GetListNoTracking());
        }

        public async Task<List<AttendanceViewModel>> GetListByScheduleId(int id)
        {
            return _mapper.Map<List<AttendanceViewModel>>(await _unitOfWork.AttendanceRepositories.GetListByScheduleIdNoTracking(id));
        }

        public Task<int> GetTotalAmount()
        {
            throw new NotImplementedException();
        }

        public Task Update(int claId, UpdateClass upCla)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateStatus(int attId, string upAttendStatus)
        {
            // Fetch the existing record
            var att = await _unitOfWork.AttendanceRepositories.GetByIdNoTracking(attId)
                ?? throw new KeyNotFoundException("Attendance not found.");

            att.HasAttended = upAttendStatus;
            // Reattach entity and mark it as modified
            _unitOfWork.AttendanceRepositories.Update(att);

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
                throw new Exception("An unexpected error occurred while updating attendant's status.", ex);
            }
        }
    }
}
