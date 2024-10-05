using CEG_BAL.ViewModels.Admin;
using CEG_BAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Interfaces
{
    public interface IHomeworkAnswerService
    {
        void Create(HomeworkAnswerViewModel model, CreateNewAnswer newAnsw);
        void Update(HomeworkAnswerViewModel model);
        Task<List<HomeworkAnswerViewModel>> GetAnswerList();
        Task<HomeworkAnswerViewModel?> GetAnswerById(int id);
    }
}
