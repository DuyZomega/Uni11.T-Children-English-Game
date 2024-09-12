﻿using CEG_BAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Interfaces
{
    public interface IGameLevel
    {
        void Create(GameLevelViewModel model);
        void Update(GameLevelViewModel model);
        Task<List<GameLevelViewModel>> GetAll();
        Task<GameLevelViewModel> GetById(int id);
    }
}
