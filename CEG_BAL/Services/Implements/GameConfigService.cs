using AutoMapper;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Admin;
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
    public class GameConfigService : IGameConfigService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GameConfigService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task Create(CreateNewGameConfig newGamCon)
        {
            if (newGamCon == null)
                throw new ArgumentNullException(nameof(newGamCon), "The new game config cannot be null.");

            var gamCon = new GameConfig();
            _mapper.Map(newGamCon, gamCon);

            // Save to the database
            try
            {
                _unitOfWork.GameConfigRepositories.Create(gamCon);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                // Log exception (if logging is configured)
                throw new Exception("An error occurred while creating the game config.", ex);
            }
        }

        public async Task<GameConfigViewModel?> GetById(int id)
        {
            var user = await _unitOfWork.GameConfigRepositories.GetByIdNoTracking(id);
            if (user != null)
            {
                var urs = _mapper.Map<GameConfigViewModel>(user);
                return urs;
            }
            return null;
        }

        public async Task<List<GameConfigViewModel>> GetList()
        {
            return _mapper.Map<List<GameConfigViewModel>>(await _unitOfWork.GameConfigRepositories.GetGameConfigsList());
        }

        public async Task Update(int gamConId, UpdateGameConfig upGamCon)
        {
            if (upGamCon == null)
                throw new ArgumentNullException(nameof(upGamCon), "New game config cannot be null.");

            // Fetch the existing record
            var stuPro = await _unitOfWork.GameConfigRepositories.GetByIdNoTracking(gamConId)
                ?? throw new KeyNotFoundException("Game config not found.");

            // Map changes from the update model to the entity
            _mapper.Map(upGamCon, stuPro);

            // Reattach entity and mark it as modified
            _unitOfWork.GameConfigRepositories.Update(stuPro);

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
                throw new Exception("An unexpected error occurred while updating the game config.", ex);
            }
        }
    }
}
