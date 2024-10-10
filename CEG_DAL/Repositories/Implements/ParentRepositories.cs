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
    public class ParentRepositories : RepositoryBase<Parent>, IParentRepositories
    {
        private readonly MyDBContext _dbContext;
        public ParentRepositories(MyDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Parent?> GetByIdNoTracking(int id)
        {
            return await _dbContext.Parents.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(par => par.ParentId == id);
        }

        public async Task<List<Parent>> GetParentList()
        {
            return await _dbContext.Parents.ToListAsync();
        }

        public async Task<Parent?> GetByEmail(string email)
        {
            return await _dbContext.Parents.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(par => par.Email == email);
        }

        public async Task<int> GetIdByUsername(string username)
        {
            var result = await (from p in _dbContext.Parents where p.Account.Username == username select p).FirstOrDefaultAsync();
            if (result != null)  return result.ParentId;
            return 0;
        }

        public async Task<Parent?> GetByAccountIdNoTracking(int id)
        {
            return await _dbContext.Parents
                .Include(t => t.Account)
                .ThenInclude(a => a.Role)
                .AsNoTrackingWithIdentityResolution()
                .SingleOrDefaultAsync(t => t.Account.AccountId == id);
        }

        public async Task<int> GetIdByAccountId(int id)
        {
            var result = await (from p in _dbContext.Parents where p.AccountId == id select p).FirstOrDefaultAsync();
            if (result != null) return result.ParentId;
            return 0;
        }
    }
}
