using AutoMapper;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
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
    public class GameConfigService : IGameConfig
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJWTService _jwtService;
        private readonly IConfiguration _configuration;

        public GameConfigService(
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
        public void Create(GameConfigViewModel model)
        {
            var game = _mapper.Map<GameConfig>(model);
            _unitOfWork.GameConfigRepositories.Create(game);
            _unitOfWork.Save();
        }

        public async Task<GameConfigViewModel> GetGameConfigById(int id)
        {
            var user = await _unitOfWork.GameConfigRepositories.GetByIdNoTracking(id);
            if (user != null)
            {
                var urs = _mapper.Map<GameConfigViewModel>(user);
                return urs;
            }
            return null;
        }

        public async Task<List<GameConfigViewModel>> GetGameConfigsList()
        {
            return _mapper.Map<List<GameConfigViewModel>>(await _unitOfWork.GameConfigRepositories.GetGameConfigsList());
        }

        public void Update(GameConfigViewModel model)
        {
            var game = _mapper.Map<GameConfig>(model);
            _unitOfWork.GameConfigRepositories.Update(game);
            _unitOfWork.Save();
        }
    }
}
