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
            return await _dbContext.Transactions
                .AsNoTrackingWithIdentityResolution()
                .SingleOrDefaultAsync(tran => tran.TransactionId == id);
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
                    Account = new Account
                    {
                        Fullname = tra.Account.Fullname
                    }
                })
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
        }

        public async Task<List<Transaction>> GetAllByParentId(int? parentId)
        {
            return await _dbContext.Transactions.Where(t => t.Account.Parents.Any(par => par.ParentId == parentId)).ToListAsync();
        }

        public async Task<List<Transaction>> GetAllByTeacherId(int? teacherId)
        {
            // Fetch all transactions where Description is not null
            var transactions = await _dbContext.Transactions
                .Where(t => t.Description != null && t.TransactionType.Equals("Earning"))
                .ToListAsync();

            // Filter transactions in-memory using the custom method
            return transactions
                .Where(t => CheckTeacherIdFromDescription(t.Description, teacherId))
                .ToList();
        }

        public async Task<Transaction?> GetByVnpayId(string? vnpayId)
        {
            return await _dbContext.Transactions.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(t => t.VnpayId == vnpayId);
        }

        public async Task<int> GetTotalAmount()
        {
            return await _dbContext.Transactions.CountAsync();
        }

        public async Task<int> GetTotalAmountByAccountId(int id)
        {
            return await _dbContext.Transactions.Where(t => t.AccountId == id).CountAsync();
        }

        public async Task<int> GetSumValue()
        {
            return await _dbContext.Transactions.SumAsync(t => t.TransactionAmount);
        }

        private static bool CheckTeacherIdFromDescription(string? des, int? teaId)
        {
            if(des == null) return false;
            string teaSec = des.Split(',')[3]; // Get Teacher section
            string teaIdLabel = "Teacher id: ";
            var teaDesId = Int32.Parse(teaSec.Substring(teaIdLabel.Length));
            return  teaDesId == teaId;
        }
    }
}
