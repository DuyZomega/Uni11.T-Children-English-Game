using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_DAL.Repositories.Interfaces
{
    public interface ISessionRepositories : IRepositoryBase<Session>
    {
        Task<List<Session>> GetSessionList();
        Task<Session?> GetByIdNoTracking(int id);
        Task<Session?> GetByTitle(string name);
        Task<int> GetIdByTitle(string name);
    }
}
