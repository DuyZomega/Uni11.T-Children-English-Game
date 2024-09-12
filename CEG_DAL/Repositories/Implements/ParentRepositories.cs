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
            return await _dbContext.Parents.AsNoTrackingWithIdentityResolution().SingleOrDefaultAsync(par => par.ParentsId == id);
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
            if (result != null)  return result.ParentsId;
            return 0;
        }

        public async Task<int> GenerateNewParentId()
        {
            var lastAcc = await _dbContext.Parents.OrderByDescending(acc => acc.ParentsId).FirstOrDefaultAsync();
            int newId = 1;
            if (lastAcc != null)
            {
                newId = lastAcc.ParentsId + 1;
            }
            return newId;
        }
    }
}
