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
        Task Create (CreateNewEnroll newEn);
        void Update (EnrollViewModel model);
        void UpdateStatus(int enrollId, string enrollStatus);
        Task<List<EnrollViewModel>> GetList();
        Task<EnrollViewModel?> GetById (int id);
        Task<List<EnrollViewModel>?> GetEnrollByParentAccountId(int id);
        Task<List<EnrollViewModel>?> GetEnrollByStudentAccountId(int id);
        Task<EnrollViewModel?> GetByStudentFullnameAndClassName(string stuFullname, string claName);
    }
}
