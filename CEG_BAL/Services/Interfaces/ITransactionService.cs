using CEG_BAL.ViewModels;
using CEG_BAL.ViewModels.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<int> Create(TransactionViewModel model, CreateTransaction newTran);
        void Update(TransactionViewModel model);
        Task<List<TransactionViewModel>> GetTransactionList();
        Task<TransactionViewModel?> GetTransactionById(int id);
        Task<List<TransactionViewModel>> GetTransactionByParentAccountId(int id);
        Task<TransactionViewModel?> GetTransactionByVnpayId(string? vnpayId);
    }
}
