using CEG_BAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Interfaces
{
    public interface IHomeworkResultService
    {
        void Create(HomeworkResultViewModel model);
        void Update(HomeworkResultViewModel model);
        Task<List<HomeworkResultViewModel>> GetAllHomeworkResult();
        Task<HomeworkResultViewModel> GetHomeworkResultById(int id);
    }
}
