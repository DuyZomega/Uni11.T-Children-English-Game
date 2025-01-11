using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_DAL.Repositories.Interfaces
{
    public interface IHomeworkAnswerRepositories : IRepositoryBase<HomeworkAnswer>
    {
        Task<List<HomeworkAnswer>> GetList();
        Task<HomeworkAnswer?> GetByIdNoTracking(int id);
        Task<HomeworkAnswer?> GetByAnswer(string answer);
        Task<int> GetIdByAnswer(string answer);
        Task<List<HomeworkAnswer>?> GetListByQuestionId(int questionId);
        Task<List<HomeworkAnswer>?> GetListBySessionId(int sessionId);
        Task<List<HomeworkAnswer>?> GetListByCourseId(int courseId);
    }
}
