using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Interfaces
{
    public interface ISessionService
    {
        void Create(SessionViewModel model, CreateNewSession newSes);
        void Update(SessionViewModel model);
        Task<List<SessionViewModel>> GetSessionList();
        Task<SessionViewModel?> GetSessionById(int id);
    }
}
