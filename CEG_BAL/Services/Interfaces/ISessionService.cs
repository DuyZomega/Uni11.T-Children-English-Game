﻿using CEG_BAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Interfaces
{
    public interface ISessionService
    {
        void Create(SessionViewModel model);
        void Update(SessionViewModel model);
        Task<List<SessionViewModel>> GetAllSessions();
        Task<SessionViewModel> GetSessionById(int id);
    }
}
