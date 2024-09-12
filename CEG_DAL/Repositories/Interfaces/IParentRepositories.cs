using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_DAL.Repositories.Interfaces
{
    public interface IParentRepositories : IRepositoryBase<Parent>
    {
        Task<List<Parent>> GetParentList();
        Task<Parent?> GetByIdNoTracking(int id);
        Task<Parent?> GetByEmail(string email);
    }
}
