using AutoMapper;
using CEG_BAL.Services.Interfaces;
using CEG_BAL.ViewModels;
using CEG_DAL.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CEG_DAL.Models;

namespace CEG_BAL.Services.Implements
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJWTService _jwtService;
        private readonly IConfiguration _configuration;

        public GameService(
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
        public void Create(GameViewModel game)
        {
            var gam = _mapper.Map<Game>(game);
            _unitOfWork.GameRepositories.Create(gam);
            _unitOfWork.Save();
        }

        public async Task<GameViewModel> GetGameById(int id)
        {
            var user = await _unitOfWork.GameRepositories.GetByIdNoTracking(id);
            if (user != null)
            {
                var urs = _mapper.Map<GameViewModel>(user);
                return urs;
            }
            return null;
        }

        public async Task<List<GameViewModel>> GetGamesList()
        {
            return _mapper.Map<List<GameViewModel>>(await  _unitOfWork.GameRepositories.GetGamesList());
        }

        public void Update(GameViewModel game)
        {
            var gam = _mapper.Map<Game>(game);
            _unitOfWork.GameRepositories.Update(gam);
            _unitOfWork.Save();
        }
    }
}
