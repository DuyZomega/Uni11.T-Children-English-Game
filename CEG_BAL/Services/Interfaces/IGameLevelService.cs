using CEG_BAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Interfaces
{
    public interface IGameLevelService
    {
        void Create(GameLevelViewModel model);
        void Update(GameLevelViewModel model);
        Task<List<GameLevelViewModel>> GetAllGameLevel();
        Task<GameLevelViewModel> GetGameLevelById(int id);
    }
}
