using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Interfaces
{
    public interface IHomeworkQuestionService
    {
        void Create(HomeworkQuestionViewModel model, CreateNewQuestion newQues);
        void CreateWithHomeworkId(HomeworkQuestionViewModel model, CreateNewQuestion newQues, int homeworkId);
        void Update(HomeworkQuestionViewModel model);
        void UpdateWithHomeworkId(int questionId, int homeworkId);
        Task<List<HomeworkQuestionViewModel>> GetList();
        Task<List<HomeworkQuestionViewModel>> GetListByCourseId(int courseId);
        Task<List<HomeworkQuestionViewModel>> GetListBySessionId(int sessionId);
        Task<List<HomeworkQuestionViewModel>?> GetOrderedList();
        Task<HomeworkQuestionViewModel?> GetById(int id);
    }
}
