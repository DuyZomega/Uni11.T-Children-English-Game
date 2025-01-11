using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_DAL.Repositories.Interfaces
{
    public interface IHomeworkResultRepositories : IRepositoryBase<HomeworkResult>
    {
        Task<List<HomeworkResult>> GetList();
        Task<HomeworkResult?> GetByIdNoTracking(int id);
        Task<HomeworkResult?> GetByStudentIdAndHomeworkIdNoTracking(int stuId, int homId);
    }
}
