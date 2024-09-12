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
    public class GameLevelService : IGameLevel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJWTService _jwtService;
        private readonly IConfiguration _configuration;

        public GameLevelService(
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
        public void Create(GameLevelViewModel model)
        {
            var game = _mapper.Map<GameLevel>(model);
            _unitOfWork.GameLevelRepositories.Create(game);
            _unitOfWork.Save();
        }

        public async Task<List<GameLevelViewModel>> GetAll()
        {
            return _mapper.Map<List<GameLevelViewModel>>(await _unitOfWork.GameLevelRepositories.GetGameLevelsList());
        }

        public async Task<GameLevelViewModel> GetById(int id)
        {
            var user = await _unitOfWork.GameLevelRepositories.GetByIdNoTracking(id);
            if(user != null)
            {
                var urs = _mapper.Map<GameLevelViewModel>(user);
                return urs;
            }
            return null;
        }

        public void Update(GameLevelViewModel model)
        {
            var game = _mapper.Map<GameLevel>(model);
            _unitOfWork.GameLevelRepositories.Update(game);
            _unitOfWork.Save();
        }
    }
}
