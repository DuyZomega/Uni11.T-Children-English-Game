using CEG_BAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Interfaces
{
    public interface ITransactionService
    {
        void Create(TransactionViewModel model);
        void Update(TransactionViewModel model);
        Task<List<TransactionViewModel>> GetTransactionList();
        Task<TransactionViewModel?> GetTransactionById(int id);
        Task<List<TransactionViewModel>> GetTransactionByParentAccountId(int id);
    }
}
