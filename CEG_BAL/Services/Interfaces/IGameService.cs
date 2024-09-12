using CEG_BAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Interfaces
{
    public interface IGameService
    {
        void Create(GameViewModel game);
        void Update(GameViewModel game);
        Task<List<GameViewModel>> GetGamesList();
        Task<GameViewModel> GetGameById(int id);
    }
}
