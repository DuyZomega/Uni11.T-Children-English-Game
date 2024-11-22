using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Parent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Interfaces
{
    public interface IEnrollService
    {
        void Create (EnrollViewModel model, CreateNewEnroll newEn);
        void Update (EnrollViewModel model);
        void UpdateStatus(int enrollId, string enrollStatus);
        Task<List<EnrollViewModel>> GetEnrollsList();
        Task<EnrollViewModel> GetEnrollById (int id);
        Task<List<EnrollViewModel>> GetEnrollByParentAccountId(int id);
    }
}
