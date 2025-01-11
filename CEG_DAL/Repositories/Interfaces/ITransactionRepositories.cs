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
        Task<List<Transaction>> GetListNoTracking();
        Task<Transaction?> GetByIdNoTracking(int id);
        Task<List<Transaction>> GetAllByParentId(int? parentId);
        Task<List<Transaction>> GetAllByTeacherId(int? teacherId);
        Task<Transaction?> GetByVnpayId(string? vnpayId);
        Task<int> GetTotalAmount();
        Task<int> GetTotalAmountByAccountId(int id);
        Task<int> GetSumValue();
    }
}
