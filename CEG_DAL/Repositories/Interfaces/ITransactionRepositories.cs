using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_DAL.Repositories.Interfaces
{
    public interface ITransactionRepositories : IRepositoryBase<Transaction>
    {
        Task<List<Transaction>> GetTransactionList();
        Task<Transaction?> GetByIdNoTracking(int id);
        Task<List<Transaction>> GetTransactionByParentId(int parentId);
    }
}
