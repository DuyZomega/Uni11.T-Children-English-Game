using CEG_BAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Interfaces
{
    public interface IHomeworkService
    {
        void Create(HomeworkViewModel model);
        void Update(HomeworkViewModel model);
        Task<List<HomeworkViewModel>> GetAllHomework();
        Task<HomeworkViewModel> GetHomeworkById(int id);
    }
}
