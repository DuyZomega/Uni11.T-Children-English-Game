using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Interfaces
{
    public interface IGameLevelService
    {
        void Create(CreateNewGameLevel model);
        void Update(GameLevelViewModel model);
        Task<List<GameLevelViewModel>> GetList();
        Task<GameLevelViewModel> GetGameLevelById(int id);
    }
}
