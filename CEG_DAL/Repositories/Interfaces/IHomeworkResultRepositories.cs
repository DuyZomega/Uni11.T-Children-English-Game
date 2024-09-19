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
        Task<List<HomeworkResult>> GetHomeworkResultsList();
        Task<HomeworkResult> GetByIdNoTracking(int id);
    }
}
