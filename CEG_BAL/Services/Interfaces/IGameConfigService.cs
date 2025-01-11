using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Admin;
using CEG_BAL.ViewModels.Admin.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Interfaces
{
    public interface IGameConfigService
    {
        Task Create(CreateNewGameConfig newGamCon);
        Task Update(int gamConId, UpdateGameConfig upGamCon);
        Task<List<GameConfigViewModel>> GetList();
        Task<GameConfigViewModel?> GetById(int id);
    }
}
