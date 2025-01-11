using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_DAL.Repositories.Interfaces
{
    public interface IHomeworkRepositories : IRepositoryBase <Homework>
    {
        Task<List<Homework>> GetHomeworksList();
        Task<Homework?> GetByIdNoTracking(int id);
        Task<Homework?> GetByTitle(string name);
        Task<int> GetIdByTitle(string name);
        Task<List<Homework>?> GetListBySessionId(int sesId);
        Task<List<Homework>> GetListBySessionIds(int[] sesId);
        Task<List<int>> GetIdListByScheduleId(int schId);
    }
}
