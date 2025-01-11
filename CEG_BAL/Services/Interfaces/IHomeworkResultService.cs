using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Admin.Create;
using CEG_BAL.ViewModels.Admin.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Interfaces
{
    public interface IHomeworkResultService
    {
        Task Create(CreateNewHomeworkResult model);
        Task Update(int id, UpdateHomeworkResult upHomRes);
        Task<List<HomeworkResultViewModel>> GetList();
        Task<HomeworkResultViewModel?> GetById(int id);
        Task<HomeworkResultViewModel?> GetByStudentIdAndHomeworkId(int stuId, int homId);
    }
}
