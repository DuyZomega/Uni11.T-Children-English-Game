using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using CEG_DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_DAL.Repositories.Implements
{
    public class TransactionRepositories : RepositoryBase<Transaction>, ITransactionRepositories
    {
        private readonly MyDBContext _dbContext;
        public TransactionRepositories(MyDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Transaction?> GetByIdNoTracking(int id)
        {
            return await _dbContext.Transactions.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(tran => tran.TransactionId == id);
        }

        public async Task<List<Transaction>> GetListNoTracking()
        {
            return await _dbContext.Transactions
                .Select( tra => new Transaction
                {
                    TransactionId = tra.TransactionId,
                    VnpayId = tra.VnpayId,
                    TransactionAmount = tra.TransactionAmount,
                    TransactionType = tra.TransactionType,
                    TransactionDate = tra.TransactionDate,
                    ConfirmDate = tra.ConfirmDate,
                    TransactionStatus = tra.TransactionStatus,
                    Parent = new Parent
                    {
                        ParentId = tra.ParentId,
                        Account = new Account
                        {
                            Fullname = tra.Parent.Account.Fullname,
                        }
                    }
                })
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
        }

        public async Task<List<Transaction>> GetTransactionByParentId(int parentId)
        {
            return await _dbContext.Transactions.Where(t => t.ParentId == parentId).ToListAsync();
        }

        public async Task<Transaction?> GetTransactionByVnpayId(string? vnpayId)
        {
            return await _dbContext.Transactions.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(t => t.VnpayId == vnpayId);
        }
    }
}
