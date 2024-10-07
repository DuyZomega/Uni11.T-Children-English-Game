using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_DAL.Repositories.Interfaces
{
    public interface IPaymentRepositories : IRepositoryBase<Payment>
    {
        Task<List<Payment>> GetPaymentList();
        Task<Payment?> GetByIdNoTracking(int id);
        Task<List<Payment>> GetPaymentByParentId(int parentId);
    }
}
