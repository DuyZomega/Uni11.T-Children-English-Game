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
    public class PaymentRepositories : RepositoryBase<Payment>, IPaymentRepositories
    {
        private readonly MyDBContext _dbContext;
        public PaymentRepositories(MyDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Payment?> GetByIdNoTracking(int id)
        {
            return await _dbContext.Payments.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(pay => pay.PaymentId == id);
        }

        public async Task<List<Payment>> GetPaymentList()
        {
            return await _dbContext.Payments.ToListAsync();
        }

        public async Task<List<Payment>> GetPaymentByParentId(int parentId)
        {
            return await _dbContext.Payments.Where(p => p.ParentId == parentId).ToListAsync();
        }
    }
}
