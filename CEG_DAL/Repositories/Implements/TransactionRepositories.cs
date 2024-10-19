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
            return await _dbContext.Transactions.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(pay => pay.TransactionId == id);
        }

        public async Task<List<Transaction>> GetTransactionList()
        {
            return await _dbContext.Transactions.ToListAsync();
        }

        public async Task<List<Transaction>> GetTransactionByParentId(int parentId)
        {
            return await _dbContext.Transactions.Where(p => p.ParentId == parentId).ToListAsync();
        }
    }
}
