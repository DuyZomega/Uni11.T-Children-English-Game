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
        Task<List<HomeworkAnswerViewModel>> GetList();
        Task<List<HomeworkAnswerViewModel>?> GetListByQuestionId(int questionId);
        Task<List<HomeworkAnswerViewModel>?> GetListBySessionId(int sessionId);
        Task<List<HomeworkAnswerViewModel>?> GetListByCourseId(int courseId);
        Task<HomeworkAnswerViewModel?> GetById(int id);
    }
}
