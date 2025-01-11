using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Interfaces
{
    public interface IGameService
    {
        void Create(CreateNewGame game);
        void Update(GameViewModel game);
        Task<List<GameViewModel>> GetGamesList();
        Task<GameViewModel> GetGameById(int id);
    }
}
